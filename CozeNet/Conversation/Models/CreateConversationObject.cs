namespace CozeNet.Conversation.Models;

public record CreateConversationObject(EnterMessageObject[]? Messages, object? MetaData);