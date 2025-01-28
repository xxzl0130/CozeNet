using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Chat.Models
{
    public class Usage
    {
        /// <summary>
        /// 本次对话消耗的 Token 总数，包括 input 和 output 部分的消耗。
        /// </summary>
        [JsonPropertyName("token_count")]
        public int TokenCount { get; set; }

        /// <summary>
        /// output 部分消耗的 Token 总数。
        /// </summary>
        [JsonPropertyName("output_count")]
        public int OutputCount { get; set; }

        /// <summary>
        /// input 部分消耗的 Token 总数。
        /// </summary>
        [JsonPropertyName("input_count")]
        public int InputCount { get; set; }
    }
}
