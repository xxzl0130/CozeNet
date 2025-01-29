using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CozeNet.Core;
using CozeNet.Knowledge.Models;
using CozeNet.Workflow.Models;

namespace CozeNet.Knowledge
{
    public class KnowledgeService
    {
        private Context _context;

        public KnowledgeService(Context context)
        {
            _context = context;
        }

        public async Task<DatasetCreateResponse?> CreateDatasetAsync(DatasetCreatRequest createRequest)
        {
            var api = "/v1/datasets";
            return await _context.GetJsonAsync<DatasetCreateResponse>(api, HttpMethod.Post, JsonContent.Create(createRequest));
        }
        public async Task<DatasetCreateResponse?> GetDatasetAsync(DatasetCreatRequest request)
        {
            var api = "/v1/datasets";
            return await _context.GetJsonAsync<DatasetCreateResponse>(api, HttpMethod.Get, JsonContent.Create(request));
        }
    }
}
