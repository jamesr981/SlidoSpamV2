using System.Text.Json.Serialization;

namespace SlidoSpam.Api.V1;

public class UserPayload
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}