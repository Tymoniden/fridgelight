namespace FridgeLight.UploadProduce.Services;

public class Produce
{
    public Guid Guid { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public List<string> Images { get; set; } = new();

    public List<Rating> Ratings { get; set; } = new();
}