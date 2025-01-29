using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Core.Models
{
    public class ResultDetail
    {
        [JsonPropertyName("logid")]
        public string? LogID { get; set; }
    }

    public class CozeResult
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
        [JsonPropertyName("detail")]
        public ResultDetail? Detail { get; set; }
    }

    public class CozeResult<T> : CozeResult where T : class
    {
        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }
}
