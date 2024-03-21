namespace FridgeLight.Shared.Services.Storage
{
    public class FileSystemService : IFileSystemService
    {
        public async Task WriteAllText(string path, string content)
        {
            await File.WriteAllTextAsync(path, content);
        }

        public async Task WriteAllContent(string path, byte[] content)
        {
            await File.WriteAllBytesAsync(path, content);
        }

        public async Task WriteAllContent(string path, Stream stream)
        {
            var buffer = new byte[1024*32];
            using( var ms = new MemoryStream())
            {
                int read;
                while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                await WriteAllContent(path, ms.ToArray());
            }
        }

        public List<string> GetFiles(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern).ToList();
        }

        public async Task<string> ReadAllText(string path)
        {
            return await File.ReadAllTextAsync(path);
        }

        public byte[] ReadAllContent(string path)
        {
            return File.ReadAllBytes(path);
        }

        public void Remove(string path)
        {
            File.Delete(path);
        }
    }
}
