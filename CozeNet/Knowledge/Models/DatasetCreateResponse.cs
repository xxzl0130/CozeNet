using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CozeNet.Core.Models;

namespace CozeNet.Knowledge.Models
{
    public class DatasetCreateData
    {
        [JsonPropertyName("dataset_id")]
        public string? DatasetID { get; set; }
    }

    public class DatasetCreateResponse : CozeResult<DatasetCreateData>
    {
    }
}
