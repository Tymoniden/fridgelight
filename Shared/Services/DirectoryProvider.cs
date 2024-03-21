using System.Diagnostics;

namespace FridgeLight.Shared.Services
{
    public class DirectoryProvider : IDirectoryProvider
    {
        public string GetRootFolder()
        {
            if (Debugger.IsAttached)
            {
                return @"C:\Users\Steib\OneDrive - prodot GmbH\Desktop\Docker\FridgeLight";
            }

            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "fridgelight");
        }

        public string GetUploadFolder()
        {
            return Path.Combine(GetRootFolder(), "Uploads");
        }

        public void SetupEnvironment()
        {
            if (!Directory.Exists(GetUploadFolder()))
            {
                Directory.CreateDirectory(GetUploadFolder());
            }
        }
    }
}
