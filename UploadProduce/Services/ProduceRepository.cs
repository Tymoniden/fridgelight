using FridgeLight.Shared.Services;
using FridgeLight.Shared.Services.Storage;
using System.IO;
using FridgeLight.Shared.Services.Image;
using static System.Net.Mime.MediaTypeNames;

namespace FridgeLight.UploadProduce.Services
{
    public class ProduceRepository : IProduceRepository
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IDirectoryProvider _directoryProvider;
        private readonly IJsonSerializerService _jsonSerializerService;
        private readonly IImageManipulationService _imageManipulationService;

        public ProduceRepository(IFileSystemService fileSystemService, IDirectoryProvider directoryProvider, IJsonSerializerService jsonSerializerService, IImageManipulationService imageManipulationService)
        {
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
            _directoryProvider = directoryProvider ?? throw new ArgumentNullException(nameof(directoryProvider));
            _jsonSerializerService = jsonSerializerService ?? throw new ArgumentNullException(nameof(jsonSerializerService));
            _imageManipulationService = imageManipulationService ?? throw new ArgumentNullException(nameof(imageManipulationService));
        }

        public async Task SaveEntry(byte[] content)
        {
            var filename = Path.Combine(_directoryProvider.GetUploadFolder() , $"{Guid.NewGuid()}.jpg" );

            await _fileSystemService.WriteAllContent(filename, content);
        }

        public async Task<string> SavePicture(Stream stream, string fileExtension, Produce produce)
        {
            var fileName = $"{produce.Guid}{fileExtension}";
            var path = Path.Combine(_directoryProvider.GetUploadFolder() , $"{fileName}" );

            byte[] imageContent;
            var buffer = new byte[1024*32];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                imageContent = ms.ToArray();
            }

            await _fileSystemService.WriteAllContent(path, imageContent);
            
            var thumbnail = _imageManipulationService.CreateThumbnail(imageContent);
            var thumbnailFileName = $"{produce.Guid}_thumbnail{fileExtension}";
            await _fileSystemService.WriteAllContent(
                Path.Combine(_directoryProvider.GetUploadFolder(), $"{thumbnailFileName}"), thumbnail);
            

            return fileName;
        }

        public Produce CreateProduce()
        {
            return new Produce();
        }

        public async Task SaveProduce(Produce produce)
        {
            var content = _jsonSerializerService.Serialize(produce);
            var path = Path.Combine(_directoryProvider.GetUploadFolder() , $"{produce.Guid}.json" );
            await _fileSystemService.WriteAllText(path, content);
        }

        public async Task<List<Produce>> ReadAllProduces()
        {
            var produces = new List<Produce>();
            foreach(var file in _fileSystemService.GetFiles(_directoryProvider.GetUploadFolder(), "*.json"))
            {
                try
                {
                    var content = await _fileSystemService.ReadAllText(file);
                    var produce = _jsonSerializerService.Deserialize<Produce>(content);
                    if (produce != null)
                    {
                        produces.Add(produce);
                    }
                }
                catch { }
            }

            return produces;
        }

        public string GetBase64Content(string path)
        {
            var file = Path.Combine(_directoryProvider.GetUploadFolder(), path);
            var content = _fileSystemService.ReadAllContent(file);
            return Convert.ToBase64String(content);
        }

        public void DeleteProduce(Produce produce)
        {
            foreach (var image in produce.Images)
            {
                _fileSystemService.Remove(Path.Combine(_directoryProvider.GetUploadFolder(), image));
            }

            _fileSystemService.Remove(Path.Combine(_directoryProvider.GetUploadFolder(), $"{produce.Guid}.json"));
        }
    }
}
