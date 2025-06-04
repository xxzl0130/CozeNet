using System.Text.Json.Serialization;

namespace CozeNet.Knowledge.Models;

public class DocumentIdsRequest
{
    [JsonPropertyName("document_ids")] public long[] DocumentIds { get; set; } = [];
}