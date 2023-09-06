using System.Text.Json.Serialization;

namespace SlidoSpam.Api.V1;

public class AuthenticationPayload
{
    [JsonPropertyName("initialAppViewer")]
    public required string InitialAppViewer { get; set; }

    [JsonPropertyName("granted_consents")]
    public required string[] GrantedConsents { get; set; }
}