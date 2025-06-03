using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class DocumentCreateRequest
    {
        /// <summary>
        /// 知识库 ID。在扣子平台中打开指定知识库页面，页面 URL 中 knowledge 参数后的数字就是知识库 ID。
        /// 例如 https://bots.bytedance.net/space/736142423532160****/knowledge/738509371792341****，知识库 ID 为 738509371792341****。
        /// </summary>
        [JsonPropertyName("dataset_id")]
        public required string DatasetId { get; set; }

        /// <summary>
        /// 待上传文件的元数据信息。数组最大长度为 10，即每次最多上传 10 个文件。
        /// 详细说明可参考 DocumentBase object。
        /// 支持的上传方式包括：离线文件。格式支持 pdf、txt、doc、docx 类型。在线网页。
        /// </summary>
        [JsonPropertyName("document_bases")]
        public DocumentBase[]? DocumentBases { get; set; }

        /// <summary>
        /// 本次上传的新文件的分段规则。
        /// 每次调用此 API 时，均需要设置新文件的分段规则。
        /// </summary>
        [JsonPropertyName("chunk_strategy")]
        public Chunkstrategy? ChunkStrategy { get; set; }

        /// <summary>
        /// 知识库类型。
        /// </summary>
        [JsonPropertyName("format_type")]
        public DataSetType FormatType { get; set; }
    }
}
