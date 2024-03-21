namespace FridgeLight.UploadProduce.Services;

public interface IProduceRepository
{
    Task SaveEntry(byte[] content);
    Task<string> SavePicture(Stream stream, string fileExtension, Produce produce);
    Produce CreateProduce();
    Task SaveProduce(Produce produce);
    Task<List<Produce>> ReadAllProduces();
    string GetBase64Content(string path);
    void DeleteProduce(Produce produce);
}