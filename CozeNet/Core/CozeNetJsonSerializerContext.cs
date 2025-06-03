using System.Text.Json.Serialization;
using CozeNet.Chat.Models;
using CozeNet.Conversation.Models;
using CozeNet.Core.Models;
using CozeNet.File.Models;
using CozeNet.Knowledge.Models;
using CozeNet.Message.Models;
using CozeNet.Workflow.Models;

namespace CozeNet.Core;

[JsonSerializable(typeof(CozeResult<ChatObject>))]
[JsonSerializable(typeof(CozeResult<FileObject>))]
[JsonSerializable(typeof(CozeResult<ConversationObject>))]
[JsonSerializable(typeof(OAuthToken))]
[JsonSerializable(typeof(CreateConversationObject))]
[JsonSerializable(typeof(TokenRequestBody))]
[JsonSerializable(typeof(ChatRequest))]
[JsonSerializable(typeof(BaseChatObject))]
[JsonSerializable(typeof(MessageListResponse))]
[JsonSerializable(typeof(MessageObject[]))]
[JsonSerializable(typeof(MessageObject))]
[JsonSerializable(typeof(MessageModifyResponse))]
[JsonSerializable(typeof(DatasetCreateResponse))]
[JsonSerializable(typeof(CozeResult))]
[JsonSerializable(typeof(CozeResult<DocumentInfo[]>))]
[JsonSerializable(typeof(DocumentListResponse))]
[JsonSerializable(typeof(DocumentProgressResponse))]
[JsonSerializable(typeof(CozeResult<PhotoInfoList>))]
[JsonSerializable(typeof(CozeResult<MessageObject>))]
[JsonSerializable(typeof(RunResponse))]
[JsonSerializable(typeof(AsyncResult))]
[JsonSerializable(typeof(DatasetCreatRequest))]
[JsonSerializable(typeof(DatasetModifyRequest))]
[JsonSerializable(typeof(DocumentCreateRequest))]
[JsonSerializable(typeof(DocumentModifyRequest))]
[JsonSerializable(typeof(DatasetListResponse))]
[JsonSerializable(typeof(ImageDescriptionRequest))]
[JsonSerializable(typeof(DocumentIdsRequest))]
[JsonSerializable(typeof(SubmitToolOutput))]
[JsonSerializable(typeof(MessageCreateRequest))]
[JsonSerializable(typeof(MessageModifyRequest))]
[JsonSerializable(typeof(RunRequest))]
[JsonSerializable(typeof(ResumeRequest))]
[JsonSerializable(typeof(ChatFlowRequest))]
[JsonSerializable(typeof(MessageEvent))]
[JsonSerializable(typeof(ErrorEvent))]
[JsonSerializable(typeof(InterruptEvent))]
internal partial class CozeNetJsonSerializerContext : JsonSerializerContext
{
}