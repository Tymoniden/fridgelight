namespace FridgeLight.Shared.Services.Storage;

public interface IFileSystemService
{
    Task WriteAllText(string path, string content);

    Task WriteAllContent(string path, byte[] content);
    Task WriteAllContent(string path, Stream stream);
    List<string> GetFiles(string path, string searchPattern);
    Task<string> ReadAllText(string path);
    byte[] ReadAllContent(string path);
    void Remove(string path);
}