using System.Text.Json.Serialization;

namespace SlidoSpam.Api.V1;

public class SpamQuestionsParameters
{
    [JsonPropertyName("eventId")]
    public uint EventId { get; set; }
    
    [JsonPropertyName("eventSectionId")]
    public uint EventSectionId { get; set; }

    [JsonPropertyName("questionCount")]
    public uint QuestionCount { get; set; }
    
    [JsonPropertyName("postAnonymously")]
    public bool PostAnonymously { get; set; }
}