using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Chat.Models
{
    public class ToolCallFunction
    {
        /// <summary>
        /// 方法名。
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// 方法参数。
        /// </summary>
        [JsonPropertyName("arguments")]
        public string? Arguments {  get; set; }
    }
}
