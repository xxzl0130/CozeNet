using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class UpdateRule
    {
        /// <summary>
        /// 在线网页是否自动更新。取值包括：0：不自动更新1：自动更新
        /// </summary>
        [JsonPropertyName("update_type")]
        public int UpdateType { get; set; }

        /// <summary>
        /// 在线网页自动更新的频率。单位为小时，最小值为 24。
        /// </summary>
        [JsonPropertyName("update_interval")]
        public int UpdateInterval { get; set; }
    }
}
