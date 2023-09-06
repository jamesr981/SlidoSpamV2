using System.Text.Json.Serialization;

namespace SlidoSpam.Api.V1;

public class EventResponse
{
    [JsonPropertyName("event_id")] public int EventId { get; set; }

    [JsonPropertyName("uuid")] public required string Uuid { get; set; }

    [JsonPropertyName("name")] public required string Name { get; set; }

    [JsonPropertyName("code")] public required string Code { get; set; }

    [JsonPropertyName("sections")] public Section[] Sections { get; set; } = Array.Empty<Section>();
}

public class Section
{
    [JsonPropertyName("event_id")] public long EventId { get; set; }

    [JsonPropertyName("event_section_id")] public long EventSectionId { get; set; }

    [JsonPropertyName("is_active")] public bool IsActive { get; set; }

    [JsonPropertyName("is_deleted")] public bool IsDeleted { get; set; }

    [JsonPropertyName("name")] public required string Name { get; set; }

    [JsonPropertyName("uuid")] public required string Uuid { get; set; }

    [JsonPropertyName("order")] public int Order { get; set; }
}