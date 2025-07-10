using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using CozeNet.Utils;

namespace CozeNet.Core
{
    public class Context
    {
        public string AccessToken { get; set; } = string.Empty;
        public string EndPoint { get; set; } = "api.coze.cn";

        public HttpClient? HttpClient { get; set; }

        public JsonSerializerOptions JsonOptions { get; set; } = new()
        {
            TypeInfoResolverChain = { CozeNetJsonSerializerContext.Default },
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        public HttpRequestMessage GenerateRequest(string api, HttpMethod method, HttpContent? content = null, Dictionary<string, string>? parameters = null, Dictionary<string, string>? headers = null, JsonSerializerOptions? jsonOptions = null)
        {
            var url = $"https://{EndPoint}{(api.StartsWith('/') ? api : $"/{api}")}";
            if (parameters != null)
            {
                var builder = new UriBuilder(url);
                var query = HttpUtility.ParseQueryString(string.Empty);
                foreach (var kv in parameters)
                {
                    query[kv.Key] = kv.Value;
                }
                builder.Query = query.ToString();
                url = builder.ToString();
            }
            var request = new HttpRequestMessage(method, url);
            request.Content = content;
            if (headers != null)
            {
                foreach (var kv in headers)
                {
                    request.Headers.Add(kv.Key, kv.Value);
                }
            }
            request.Headers.Add(AuthorizationHeader, AuthorizationHeaderValue);
            return request;
        }

        public async Task<HttpResponseMessage> SendRequestAsync(string api, HttpMethod method, HttpContent? content = null, Dictionary<string, string>? parameters = null, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        {
            using var request = GenerateRequest(api, method, content, parameters, headers);
            return await HttpClient!.SendAsync(request, cancellationToken);
        }

        public async Task<T?> GetJsonAsync<T>(string api, HttpMethod method, HttpContent? content = null,
            Dictionary<string, string>? parameters = null, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        {
            using var request = GenerateRequest(api, method, content, parameters, headers);
            using var response = await HttpClient!.SendAsync(request, cancellationToken);
            return await response.GetJsonObjectAsync<T>(JsonOptions);
        }

        public const string AuthorizationHeader = "Authorization";
        public string AuthorizationHeaderValue => $"Bearer {AccessToken}";
    }
}
