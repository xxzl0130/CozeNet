using CozeNet.Core;
using CozeNet.Workflow.Models;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using CozeNet.Chat;

namespace CozeNet.Workflow
{
    public class WorkflowService(Context context, ChatService chatService)
    {
        /// <summary>
        /// 执行已发布的工作流。
        /// 非流式
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RunResponse?> RunAsync(RunRequest request, CancellationToken cancellationToken = default)
        {
            const string api = "/v1/workflow/run";
            request.IsAsync = false;
            return await context.GetJsonAsync<RunResponse>(api, HttpMethod.Post, JsonContent.Create(request, options: context.JsonOptions), cancellationToken: cancellationToken);
        }

        private async IAsyncEnumerable<Models.StreamMessage> ProcessStream(HttpResponseMessage response,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var reader = new StreamReader(stream);

            StreamMessage message = new();

            while (!reader.EndOfStream)
            {
                if (cancellationToken.IsCancellationRequested)
                    yield break;

                var line = await reader.ReadLineAsync(cancellationToken);
                if (string.IsNullOrEmpty(line))
                    continue;

                if (line.StartsWith("id:"))
                {
                    var idStr = line["id:".Length..];
                    message.ID = int.Parse(idStr);
                }
                else if (line.StartsWith("event:"))
                {
                    var eventStr = line["event:".Length..].Trim();
                    var eventEnum = eventStr.ToWorkflowStreamEvent();
                    if (eventEnum == Models.StreamEvents.None)
                        yield break;
                    message = new Models.StreamMessage { Event = eventEnum };
                }
                else if (line.StartsWith("data:"))
                {
                    var dataStr = line["data:".Length..].Trim();
                    message.Data = message.Event switch
                    {
                        StreamEvents.Message => JsonSerializer.Deserialize<MessageEvent>(dataStr, options: context.JsonOptions),
                        StreamEvents.Error => JsonSerializer.Deserialize<ErrorEvent>(dataStr, options: context.JsonOptions),
                        StreamEvents.Interrupt => JsonSerializer.Deserialize<InterruptEvent>(dataStr, options: context.JsonOptions),
                        _ => null
                    };

                    yield return message;
                }
            }
        }

        /// <summary>
        /// 执行已发布的工作流，响应方式为流式响应。
        /// </summary>
        /// <param name="runRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<Models.StreamMessage> RunStreamAsync(RunRequest runRequest,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var api = "/v1/workflow/run";
            runRequest.IsAsync = true;
            using var request = context.GenerateRequest(api, HttpMethod.Post, JsonContent.Create(runRequest, options: context.JsonOptions));
            using var response = await context.HttpClient!.SendAsync(request);
            await foreach (var item in ProcessStream(response, cancellationToken))
            {
                yield return item;
            }
        }

        /// <summary>
        /// 恢复运行已中断的工作流。
        /// </summary>
        /// <param name="resumeRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<Models.StreamMessage> ResumeStreamAsync(ResumeRequest resumeRequest,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var api = "/v1/workflow/stream_resume";
            using var request = context.GenerateRequest(api, HttpMethod.Post, JsonContent.Create(resumeRequest, options: context.JsonOptions));
            using var response = await context.HttpClient!.SendAsync(request, cancellationToken);
            await foreach (var item in ProcessStream(response, cancellationToken))
            {
                yield return item;
            }
        }

        /// <summary>
        /// 工作流异步运行后，查看执行结果。
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="executeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AsyncResult?> GetAsyncResultAsync(string workflowId, string executeId, CancellationToken cancellationToken = default)
        {
            var api = $"/v1/workflows/{workflowId}/run_histories/{executeId}";
            return await context.GetJsonAsync<AsyncResult>(api, HttpMethod.Get, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 执行已发布的对话流。
        /// </summary>
        /// <param name="chatFlowRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<Chat.Models.StreamMessage> RunChatFlowAsync(ChatFlowRequest chatFlowRequest,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            const string api = "/v1/workflows/chat";
            using var request = context.GenerateRequest(api, HttpMethod.Post, JsonContent.Create(chatFlowRequest, options: context.JsonOptions));
            using var response = await context.HttpClient!.SendAsync(request, HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);
            response.EnsureSuccessStatusCode();
            await foreach (var item in chatService.ProcessStreamResponce(response, cancellationToken))
            {
                yield return item;
            }
        }
    }
}