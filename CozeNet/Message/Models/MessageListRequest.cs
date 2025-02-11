using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Message.Models
{
    public class MessageListRequest
    {
        /// <summary>
        /// 消息列表的排序方式。desc：（默认）按创建时间倒序asc：按创建时间正序
        /// </summary>
        [JsonPropertyName("order")]
        public string? Order { get; set; }

        /// <summary>
        /// 待查看的 Chat ID。
        /// </summary>
        [JsonPropertyName("chat_id")]
        public string? ChatID { get; set; }

        /// <summary>
        /// 查看指定位置之前的消息。默认为 0，表示不指定位置。如需向前翻页，则指定为返回结果中的 first_id。
        /// </summary>
        [JsonPropertyName("before_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BeforeID { get; set; }

        /// <summary>
        /// 查看指定位置之后的消息。默认为 0，表示不指定位置。如需向后翻页，则指定为返回结果中的 last_id。
        /// </summary>
        [JsonPropertyName("after_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AfterID { get; set; }

        /// <summary>
        /// 每次查询返回的数据量。默认为 50，取值范围为 1~50。
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; } = 50;
    }
}
