using System.Text.Json.Serialization;

namespace Bifrost.Launcher
{
    /// <summary>
    /// <see cref="JsonSerializerContext"/> implementation for AOT publish compatibility.
    /// </summary>
    [JsonSerializable(typeof(List<Server>))]
    [JsonSerializable(typeof(LaunchConfig))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public partial class JsonContext : JsonSerializerContext
    {
    }
}
