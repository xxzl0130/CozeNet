using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Message.Models
{
    public class ObjectString
    {
        /// <summary>
        /// 多模态消息内容类型，支持设置为：
        /// text：文本类型。
        /// file：文件类型。
        /// image：图片类型。
        /// audio：音频类型
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// 文本内容。
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        /// <summary>
        /// 文件、图片、音频内容的 ID。
        /// </summary>
        [JsonPropertyName("file_id")]
        public string? FileID { get; set; }

        /// <summary>
        /// 文件或图片内容的在线地址。必须是可公共访问的有效地址。
        /// 在 type 为 file、image 或 audio 时，file_id 和 file_url 应至少指定一个。
        /// </summary>
        [JsonPropertyName("file_url")]
        public string? FileUrl { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
