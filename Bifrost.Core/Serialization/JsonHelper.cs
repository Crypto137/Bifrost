using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Bifrost.Core.Serialization
{
    public static class JsonHelper
    {
        public static T LoadOrCreate<T>(string fileName, JsonTypeInfo<T> jsonTypeInfo) where T : new()
        {
            if (File.Exists(fileName) == false)
                return new();

            using FileStream fs = File.OpenRead(fileName);
            T @object = JsonSerializer.Deserialize(fs, jsonTypeInfo);
            return @object;
        }

        public static void Save<T>(T @object, string fileName, JsonTypeInfo<T> jsonTypeInfo) where T : new()
        {
            using FileStream fs = File.Open(fileName, FileMode.Create);
            JsonSerializer.Serialize(fs, @object, jsonTypeInfo);
        }
    }
}
