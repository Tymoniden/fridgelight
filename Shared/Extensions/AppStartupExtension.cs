using FridgeLight.Shared.Services;

namespace FridgeLight.Shared.Extensions
{
    public static class AppStartupExtension
    {
        public static void SetupFolderStructure(this WebApplication? webApplication)
        {
            webApplication?.Services.GetService<IDirectoryProvider>()?.SetupEnvironment();
        }
    }
}
