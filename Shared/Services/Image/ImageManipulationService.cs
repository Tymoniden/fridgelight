using ImageMagick;

namespace FridgeLight.Shared.Services.Image
{
    public class ImageManipulationService : IImageManipulationService
    {
        private const int MaxWidth = 200;
        private const int MaxHeight = 200;

        public byte[] CreateThumbnail(byte[] image)
        {
            return ResizeImage(image, MaxWidth, MaxHeight);
        }

        public byte[] ResizeImage(byte[] image, int maxWidth, int maxHeight)
        {
            try
            {
                using var imageMagick = new MagickImage(image);
                imageMagick.Transparent(MagickColor.FromRgb(0, 0, 0));
                imageMagick.FilterType = FilterType.Quadratic;
                imageMagick.Resize(maxWidth, maxHeight);

                return imageMagick.ToByteArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
