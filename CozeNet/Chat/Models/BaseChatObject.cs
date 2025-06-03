using System.Text.Json.Serialization;

namespace CozeNet.Chat.Models;

public class BaseChatObject
{
    /// <summary>
    /// 对话 ID，即对话的唯一标识。
    /// </summary>
    [JsonPropertyName("chat_id")]
    public string? ChatID { get; set; }

    /// <summary>
    /// 会话 ID，即会话的唯一标识。
    /// </summary>
    [JsonPropertyName("conversation_id")]
    public string? ConversationID { get; set; }
}