using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class DocumentModifyRequest
    {
        /// <summary>
        /// 待修改的知识库文件 ID。
        /// </summary>
        [JsonPropertyName("document_id")]
        public string? DocumentId { get; set; }

        /// <summary>
        /// 知识库文件的新名称。
        /// </summary>
        [JsonPropertyName("document_name")]
        public string? DocumentName { get; set; }

        /// <summary>
        /// 在线网页更新策略，仅在上传在线网页时需要设置。默认不自动更新。详细信息可参考 UpdateRule object。
        /// </summary>
        [JsonPropertyName("update_rule")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public UpdateRule? UpdateRule { get; set; }
    }
}
