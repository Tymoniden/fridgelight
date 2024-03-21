namespace FridgeLight.Shared.Services.Image;

public interface IImageManipulationService
{
    byte[] CreateThumbnail(byte[] image);
    byte[] ResizeImage(byte[] image, int maxWidth, int maxHeight);
}