using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class DatasetListData
    {
        /// <summary>
        /// 空间中的知识库总数量。
        /// </summary>
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
    }

    public class DatasetListResponse
    {
    }
}
