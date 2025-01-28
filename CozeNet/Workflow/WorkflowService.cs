using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozeNet.Core;
using CozeNet.Workflow.Models;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using CozeNet.Chat.Models;
using CozeNet.Chat;

namespace CozeNet.Workflow
{
    public class WorkflowService
    {
        private Context _context;

        public WorkflowService(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// 执行已发布的工作流。
        /// 非流式
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RunResponse?> RunAsync(RunRequest request)
        {
            var api = "/v1/workflow/run";
            request.IsAsync = false;
            return await _context.GetJsonAsync<RunResponse>(api, HttpMethod.Post, JsonContent.Create(request));
        }

        private static async IAsyncEnumerable<Models.StreamMessage> ProcessStream(HttpResponseMessage response, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var reader = new StreamReader(stream);

            Models.StreamMessage message = new ();

            while (!reader.EndOfStream)
            {
                if (cancellationToken.IsCancellationRequested)
                    yield break;

                var line = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(line))
                    continue;

                if (line.StartsWith("id:"))
                {
                    var idStr = line.Substring("id:".Length);
                    message.ID = int.Parse(idStr);
                }
                else if (line.StartsWith("event:"))
                {
                    var eventStr = line.Substring("event:".Length).Trim();
                    var eventEnum = eventStr.ToWorkflowStreamEvent();
                    if (eventEnum == Models.StreamEvents.None)
                        yield break;
                    message = new Models.StreamMessage { Event = eventEnum };
                }
                else if (line.StartsWith("data:"))
                {
                    var dataStr = line.Substring("data:".Length).Trim();
                    switch (message.Event)
                    {
                        case Models.StreamEvents.Message:
                            message.Data = JsonSerializer.Deserialize<MessageEvent>(dataStr);
                            break;
                        case Models.StreamEvents.Error:
                            message.Data = JsonSerializer.Deserialize<ErrorEvent>(dataStr);
                            break;
                        case Models.StreamEvents.Interrupt:
                            message.Data = JsonSerializer.Deserialize<InterruptEvent>(dataStr);
                            break;
                        default:
                            message.Data = null;
                            break;
                    }
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
        public async IAsyncEnumerable<Models.StreamMessage> RunStreamAsync(RunRequest runRequest, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var api = "/v1/workflow/run";
            runRequest.IsAsync = true;
            using var request = _context.GenerateRequest(api, HttpMethod.Post, JsonContent.Create(runRequest));
            using var response = await _context.HttpClient!.SendAsync(request);
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
        public async IAsyncEnumerable<Models.StreamMessage> ResumeStreamAsync(ResumeRequest resumeRequest, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var api = "/v1/workflow/stream_resume";
            using var request = _context.GenerateRequest(api, HttpMethod.Post, JsonContent.Create(resumeRequest));
            using var response = await _context.HttpClient!.SendAsync(request);
            await foreach (var item in ProcessStream(response, cancellationToken))
            {
                yield return item;
            }
        }

        /// <summary>
        /// 工作流异步运行后，查看执行结果。
        /// </summary>
        /// <param name="workflowID"></param>
        /// <param name="executeID"></param>
        /// <returns></returns>
        public async Task<AsyncResult?> GetAsyncResultAsync(string workflowID, string executeID)
        {
            var api = $"/v1/workflows/{workflowID}/run_histories/{executeID}";
            return await _context.GetJsonAsync<AsyncResult>(api, HttpMethod.Get);
        }

        /// <summary>
        /// 执行已发布的对话流。
        /// </summary>
        /// <param name="chatFlowRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<Chat.Models.StreamMessage> RunChatFlowAsync(ChatFlowRequest chatFlowRequest, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var api = "/v1/workflows/chat";
            using var request = _context.GenerateRequest(api, HttpMethod.Post, JsonContent.Create(chatFlowRequest));
            using var response = await _context.HttpClient!.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            await foreach (var item in ChatService.ProcessStreamResponce(response, cancellationToken))
            {
                yield return item;
            }
        }
    }
}
