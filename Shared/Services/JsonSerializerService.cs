using System.Text.Json;

namespace FridgeLight.Shared.Services
{
    public class JsonSerializerService : IJsonSerializerService
    {
        private readonly JsonSerializerOptions _serializerOptions;

        public JsonSerializerService()
        {
            _serializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        }

        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj, _serializerOptions);
        }

        public T? Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
