using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace CozeNet.Utils
{
    internal static class HttpExtend
    {
        public static async Task<T?> GetJsonObjectAsync<T>(this HttpResponseMessage? response, JsonSerializerOptions options)
        {
            if (response is not { IsSuccessStatusCode: true })
                return default(T);
            var str = await response.Content.ReadAsStringAsync();
            return string.IsNullOrEmpty(str) ? default : JsonSerializer.Deserialize<T>(str, options);
        }
    }
}
