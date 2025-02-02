using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CozeNet.Core;
using CozeNet.Core.Models;
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

        /// <summary>
        /// 调用此接口以创建一个知识库。
        /// </summary>
        /// <param name="createRequest"></param>
        /// <returns></returns>
        public async Task<DatasetCreateResponse?> CreateDatasetAsync(DatasetCreatRequest request)
        {
            var api = "/v1/datasets";
            return await _context.GetJsonAsync<DatasetCreateResponse>(api, HttpMethod.Post, JsonContent.Create(request));
        }

        /// <summary>
        /// 调用此接口查看指定空间资源库下的全部知识库。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DatasetCreateResponse?> GetDatasetAsync(DatasetCreatRequest request)
        {
            var api = "/v1/datasets";
            return await _context.GetJsonAsync<DatasetCreateResponse>(api, HttpMethod.Get, JsonContent.Create(request));
        }

        /// <summary>
        /// 调用此接口修改知识库信息。
        /// </summary>
        /// <param name="datasetID"></param>
        /// <param name="datasetModifyRequest"></param>
        /// <returns></returns>
        public async Task<CozeResult?> ModifyDatasetAsync(string datasetID, DatasetModifyRequest request)
        {
            var api = $"/v1/datasets/{datasetID}";
            return await _context.GetJsonAsync<CozeResult>(api, HttpMethod.Put, JsonContent.Create(request));
        }

        /// <summary>
        /// 调用此接口删除知识库。
        /// </summary>
        /// <param name="datasetID"></param>
        /// <returns></returns>
        public async Task<CozeResult?> DeleteDatasetAsync(string datasetID)
        {
            var api = $"/v1/datasets/{datasetID}";
            return await _context.GetJsonAsync<CozeResult>(api, HttpMethod.Delete);
        }

        /// <summary>
        /// 调用此接口向指定知识库中上传文件。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CozeResult<DocumentInfo[]>?> CreateDocumentAsync(DocumentCreateRequest request)
        {
            var api = "/open_api/knowledge/document/create";
            return await _context.GetJsonAsync<CozeResult<DocumentInfo[]>?>(api, HttpMethod.Post, JsonContent.Create(request),
                headers: new Dictionary<string, string> { { "Agw-Js-Conv", "str" } });
        }

        /// <summary>
        /// 调用接口修改知识库文件名称和更新策略。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CozeResult<DocumentInfo>?> ModifyDocumentAsync(DocumentModifyRequest request)
        {
            var api = "/open_api/knowledge/document/update";
            return await _context.GetJsonAsync<CozeResult<DocumentInfo>?>(api, HttpMethod.Post, JsonContent.Create(request),
                headers: new Dictionary<string, string> { { "Agw-Js-Conv", "str" } });
        }

        /// <summary>
        /// 调用接口查看指定知识库的文件列表，即文档、表格或图像列表。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DocumentListResponse?> ListDocumentAsync(DocumentListResponse request)
        {
            var api = "/open_api/knowledge/document/list";
            return await _context.GetJsonAsync<DocumentListResponse?>(api, HttpMethod.Post, JsonContent.Create(request),
                headers: new Dictionary<string, string> { { "Agw-Js-Conv", "str" } });
        }

        /// <summary>
        /// 调用此接口获取知识库文件的上传进度。
        /// 此接口支持查看所有类型知识库文件的上传进度，例如文本、图片、表格。
        /// 支持批量查看多个文件的进度，多个文件必须位于同一个知识库中。
        /// </summary>
        /// <param name="datasetID"></param>
        /// <returns></returns>
        public async Task<DocumentProgressResponse?> GetDocumentProgressAsync(string datasetID)
        {
            var api = $"/v1/datasets/{datasetID}/process";
            return await _context.GetJsonAsync<DocumentProgressResponse?>(api, HttpMethod.Post);
        }

        /// <summary>
        /// 调用此接口更新知识库中图片的描述信息。
        /// </summary>
        /// <param name="datasetID"></param>
        /// <param name="documentID"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<CozeResult?> ModifyImageDescription(string datasetID, string documentID, string description)
        {
            var api = $"/v1/datasets/{datasetID}/images/{documentID}";
            var body = new { caption = description };
            return await _context.GetJsonAsync<CozeResult?>(api, HttpMethod.Post, JsonContent.Create(body));
        }

        /// <summary>
        /// 调用此接口查看图片类知识库中图片的详细信息。
        /// 查看图片时，支持通过图片的标注进行筛选。
        /// </summary>
        /// <param name="datasetID"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <param name="hasCaption"></param>
        /// <returns></returns>
        public async Task<CozeResult<PhotoInfoList>?> GetPhotoListAsync(string datasetID, int? pageNum = null, int? pageSize = null, string? keyword = null, bool? hasCaption = null)
        {
            var api = $"/v1/datasets/{datasetID}/images";
            var parameters = new Dictionary<string, string>();
            if (pageNum.HasValue)
                parameters["page_num"] = pageNum.Value.ToString();
            if (pageSize.HasValue)
                parameters["page_size"] = pageSize.Value.ToString();
            if (!string.IsNullOrEmpty(keyword))
                parameters["keyword"] = keyword;
            if (hasCaption.HasValue)
                parameters["has_caption"] = hasCaption.Value.ToString();
            return await _context.GetJsonAsync<CozeResult<PhotoInfoList>?>(api, HttpMethod.Get, parameters: parameters);
        }

        /// <summary>
        /// 调用此接口删除知识库中的文本、图片、表格等文件，支持批量删除。
        /// </summary>
        /// <param name="documentIDs"></param>
        /// <returns></returns>
        public async Task<CozeResult?> DeleteDocumentsAsync(IEnumerable<long> documentIDs)
        {
            var api = "/open_api/knowledge/document/delete";
            var body = new
            {
                document_ids = documentIDs.ToArray()
            };
            return await _context.GetJsonAsync<CozeResult>(api, HttpMethod.Post, JsonContent.Create(body));
        }
    }
}
