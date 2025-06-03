using System.Text.Json.Serialization;

namespace CozeNet.Chat.Models;

public class SubmitToolOutput
{
    [JsonPropertyName("tool_outputs")] public ToolOutput[] ToolOutputs { get; set; } = [];
    [JsonPropertyName("stream")] public bool Stream { get; set; }
}