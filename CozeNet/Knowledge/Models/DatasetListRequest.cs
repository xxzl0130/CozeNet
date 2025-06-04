using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class DatasetListRequest
    {
        public string? SpaceID { get; set; }

        public string? Name { get; set; }

        public DataSetType FormatType { get; set; } = DataSetType.Text;

        public int PageNum { get; set; } = 1;

        public int PageSize { get; set; } = 5;
    }
}
