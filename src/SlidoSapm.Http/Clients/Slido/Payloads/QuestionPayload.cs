using System.Text.Json.Serialization;

namespace SlidoSpam.Api.V1;

public class QuestionPayload
{
    [JsonPropertyName("event_id")]
    public long EventId { get; set; }
    
    [JsonPropertyName("event_section_id")]
    public long EventSectionId { get; set; }
    
    [JsonPropertyName("is_anonymous")]
    public bool IsAnonymous { get; set; }

    [JsonPropertyName("labels")]
    public string[] Labels { get; set; } = Array.Empty<string>();

    [JsonPropertyName("path")] 
    public string Path { get; set; } = "/questions";
    
    [JsonPropertyName("text")] 
    public required string Text { get; set; }
}