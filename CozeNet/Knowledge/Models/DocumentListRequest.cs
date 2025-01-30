using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class DocumentListRequest
    {
        /// <summary>
        /// 待查看文件的知识库 ID。
        /// </summary>
        [JsonPropertyName("dataset_id")]
        public long DatasetID {  get; set; }

        /// <summary>
        /// 分页查询时的页码。默认为 1，即从第一页数据开始返回。
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        /// <summary>
        /// 分页大小。默认为 10，即每页返回 10 条数据。
        /// </summary>
        [JsonPropertyName("size")]
        public int Size { get; set; } = 10;
    }
}
