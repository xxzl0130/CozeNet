using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace CozeNet.Utils
{
    internal static class HttpExtend
    {
        public static async Task<T?> GetJsonObjectAsync<T>(this HttpResponseMessage response)
        {
            if (response == null || !response.IsSuccessStatusCode)
                return default(T);
            var str = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(str))
                return default(T);
            return JsonSerializer.Deserialize<T>(str);
        }
    }
}
