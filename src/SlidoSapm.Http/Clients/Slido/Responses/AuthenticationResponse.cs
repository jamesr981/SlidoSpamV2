using System.Text.Json.Serialization;

namespace SlidoSpam.Api.V1;

public class AuthenticationResponse
{
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; set; }
    
    [JsonPropertyName("event_id")]
    public long EventId { get; set; }
    
    [JsonPropertyName("event_user_id")]
    public long EventUserId { get; set; }
}