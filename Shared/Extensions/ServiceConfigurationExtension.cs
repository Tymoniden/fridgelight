using FridgeLight.Shared.Services;
using FridgeLight.Shared.Services.Image;
using FridgeLight.Shared.Services.Storage;
using FridgeLight.UploadProduce.Services;

namespace FridgeLight.Shared.Extensions
{
    public static class ServiceConfigurationExtension
    {
        public static void AddFridgeLightServices(this IServiceCollection serviceCollection)
        {
            ConfigureFactories(serviceCollection);
            ConfigureSharedServices(serviceCollection);
            ConfigureUploadEntryServices(serviceCollection);
        }

        private static void ConfigureUploadEntryServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IProduceRepository, ProduceRepository>();
        }

        public static void ConfigureSharedServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IFileSystemService, FileSystemService>();
            serviceCollection.AddSingleton<IDirectoryProvider, DirectoryProvider>();
            serviceCollection.AddSingleton<IJsonSerializerService, JsonSerializerService>();
            serviceCollection.AddSingleton<IImageManipulationService, ImageManipulationService>();
        }

        public static void ConfigureFactories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<DiagnosticService>();
        }
    }
}
