namespace FridgeLight.Shared.Services;

public interface IJsonSerializerService
{
    string Serialize(object obj);
    T? Deserialize<T>(string json);
}