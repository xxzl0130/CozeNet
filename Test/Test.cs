using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozeNet.Core;
using CozeNet.Core.Authorization;
using CozeNet.Conversation;
using CozeNet.Chat;
using CozeNet.Message.Models;
using CozeNet.Message;
using CozeNet.Chat.Models;

namespace Test;

internal class Test
{
    static public async Task Run(Options options)
    {
        IAuthorization authorization = new JWTAuthorization(options.AppID, options.EndPoint, options.PublicKey, options.PrivateKey);
        var context = new Context()
        {
            AccessToken = await authorization.GetAccessTokenAsync(),
            EndPoint = options.EndPoint,
            HttpClient = new HttpClient()
        };
        Console.WriteLine(context.AccessToken);
        var conversionService = new ConversationService(context);
        var chatService = new ChatService(context);
        var messageService = new MessageService(context);
        var conversion = await conversionService.CreateAsync();
        Console.WriteLine($"Conversion ID: {conversion.Data.ID}");
        var question = "你好，我有一个红色的帽衫";
        var chatResponse = await chatService.SendNoneStreamAsync(conversion.Data.ID, new CozeNet.Chat.Models.ChatRequest
        {
            BotID = options.BotID,
            UserID = "1",
            AutoSaveHistory = true,
            Stream = false,
            AdditionalMessages = new List<CozeNet.Message.Models.BaseMessageObject>
            {
                new CozeNet.Message.Models.BaseMessageObject
                {
                    Role = RoleType.User,
                    Type = MessageType.Question,
                    Content = question,
                    ContentType = ContentType.Text,
                }
            }
        });
        while (true)
        {
            var status = await chatService.RetrieveAsync(conversion.Data.ID, chatResponse.Data.ID);
            Console.WriteLine(status?.Data?.Status);
            if (status?.Data?.Status != "completed")
            {
                await Task.Delay(1000);
                continue;
            }
            break;
        }
        var messages = await messageService.ListAsync(conversion.Data.ID, new MessageListRequest { ChatID = chatResponse.Data.ID });
        foreach (var msg in messages!.Data!)
        {
            Console.WriteLine($"{msg.Role} {msg.Type}: {msg.Content}");
        }
        var stream = chatService.SendStreamAsync(conversion.Data.ID, new CozeNet.Chat.Models.ChatRequest
        {
            BotID = options.BotID,
            UserID = "1",
            AutoSaveHistory = true,
            Stream = false,
            AdditionalMessages = new List<CozeNet.Message.Models.BaseMessageObject>
            {
                new CozeNet.Message.Models.BaseMessageObject
                {
                    Role = RoleType.User,
                    Type = MessageType.Question,
                    Content = "你知道我的帽衫是什么颜色吗？",
                    ContentType = ContentType.Text,
                },
            }
        });
        string content = "";
        await foreach (var msg in stream)
        {
            Console.Write($"{msg.Event} ");
            if (msg.Event == CozeNet.Chat.Models.StreamEvents.DeltaMessage || msg.Event == CozeNet.Chat.Models.StreamEvents.MessageComplete)
            {
                var message = msg.Data as MessageObject;
                Console.WriteLine(message.Content);
                if (msg.Event == CozeNet.Chat.Models.StreamEvents.DeltaMessage)
                    content += message.Content;
            }
            else if (msg.Event == CozeNet.Chat.Models.StreamEvents.ChatComplete)
            {
                var chat = msg.Data as ChatObject;
                Console.WriteLine($"Chat complete, usage: token count {chat.Usage.TokenCount}, output count {chat.Usage.OutputCount}, input count {chat.Usage.InputCount}")
            }
            else
            {
                Console.WriteLine();
            }
        }
        Console.WriteLine(content);
    }
}
