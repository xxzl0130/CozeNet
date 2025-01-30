using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class DocumentBase
    {
        /// <summary>
        /// 文件名称。
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// 文件的元数据信息。详细信息可参考 SourceInfo object。
        /// </summary>
        [JsonPropertyName("source_info")]
        public SourceInfo? SourceInfo { get; set; }

        /// <summary>
        /// 在线网页的更新策略。默认不自动更新。详细信息可参考 UpdateRule object。
        /// </summary>
        [JsonPropertyName("update_rule")]
        public UpdateRule? UpdateRule { get; set; }
    }
}
