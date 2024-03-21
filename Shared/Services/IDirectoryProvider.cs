namespace FridgeLight.Shared.Services;

public interface IDirectoryProvider
{
    string GetRootFolder();
    string GetUploadFolder();
    void SetupEnvironment();
}