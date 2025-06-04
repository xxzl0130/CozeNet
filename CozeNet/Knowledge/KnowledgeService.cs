using System.Net.Http.Json;
using CozeNet.Core;
using CozeNet.Core.Models;
using CozeNet.Knowledge.Models;

namespace CozeNet.Knowledge
{
    public class KnowledgeService(Context context)
    {
        /// <summary>
        /// 调用此接口以创建一个知识库。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DatasetCreateResponse?> CreateDatasetAsync(DatasetCreatRequest request, CancellationToken cancellationToken = default)
        {
            var api = "/v1/datasets";
            return await context.GetJsonAsync<DatasetCreateResponse>(api, HttpMethod.Post, JsonContent.Create(request, options: context.JsonOptions), cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 调用此接口查看指定空间资源库下的全部知识库。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DatasetCreateResponse?> GetDatasetAsync(DatasetCreatRequest request, CancellationToken cancellationToken = default)
        {
            var api = "/v1/datasets";
            return await context.GetJsonAsync<DatasetCreateResponse>(api, HttpMethod.Get, JsonContent.Create(request, options: context.JsonOptions), cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 调用此接口修改知识库信息。
        /// </summary>
        /// <param name="datasetId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult?> ModifyDatasetAsync(string datasetId, DatasetModifyRequest request, CancellationToken cancellationToken = default)
        {
            var api = $"/v1/datasets/{datasetId}";
            return await context.GetJsonAsync<CozeResult>(api, HttpMethod.Put, JsonContent.Create(request, options: context.JsonOptions), cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 调用此接口删除知识库。
        /// </summary>
        /// <param name="datasetId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult?> DeleteDatasetAsync(string datasetId, CancellationToken cancellationToken = default)
        {
            var api = $"/v1/datasets/{datasetId}";
            return await context.GetJsonAsync<CozeResult>(api, HttpMethod.Delete, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 调用此接口向指定知识库中上传文件。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CozeResult<DocumentInfo[]>?> CreateDocumentAsync(DocumentCreateRequest request, CancellationToken cancellationToken = default)
        {
            const string api = "/open_api/knowledge/document/create";
            return await context.GetJsonAsync<CozeResult<DocumentInfo[]>?>(api, HttpMethod.Post, JsonContent.Create(request, options: context.JsonOptions),
                headers: new Dictionary<string, string> { { "Agw-Js-Conv", "str" } }, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 调用接口修改知识库文件名称和更新策略。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult<DocumentInfo>?> ModifyDocumentAsync(DocumentModifyRequest request, CancellationToken cancellationToken = default)
        {
            const string api = "/open_api/knowledge/document/update";
            return await context.GetJsonAsync<CozeResult<DocumentInfo>?>(api, HttpMethod.Post, JsonContent.Create(request, options: context.JsonOptions),
                headers: new Dictionary<string, string> { { "Agw-Js-Conv", "str" } }, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 调用接口查看指定知识库的文件列表，即文档、表格或图像列表。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DocumentListResponse?> ListDocumentAsync(DocumentListResponse request, CancellationToken cancellationToken = default)
        {
            const string api = "/open_api/knowledge/document/list";
            return await context.GetJsonAsync<DocumentListResponse?>(api, HttpMethod.Post, JsonContent.Create(request, options: context.JsonOptions),
                headers: new Dictionary<string, string> { { "Agw-Js-Conv", "str" } }, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 调用此接口获取知识库文件的上传进度。
        /// 此接口支持查看所有类型知识库文件的上传进度，例如文本、图片、表格。
        /// 支持批量查看多个文件的进度，多个文件必须位于同一个知识库中。
        /// </summary>
        /// <param name="datasetId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DocumentProgressResponse?> GetDocumentProgressAsync(string datasetId, CancellationToken cancellationToken = default)
        {
            var api = $"/v1/datasets/{datasetId}/process";
            return await context.GetJsonAsync<DocumentProgressResponse?>(api, HttpMethod.Post, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 调用此接口更新知识库中图片的描述信息。
        /// </summary>
        /// <param name="datasetId"></param>
        /// <param name="documentId"></param>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult?> ModifyImageDescription(string datasetId, string documentId, string description, CancellationToken cancellationToken = default)
        {
            var api = $"/v1/datasets/{datasetId}/images/{documentId}";
            var body = new ImageDescriptionRequest { Caption = description };
            return await context.GetJsonAsync<CozeResult?>(api, HttpMethod.Post, JsonContent.Create(body, options: context.JsonOptions), cancellationToken: cancellationToken);
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult<PhotoInfoList>?> GetPhotoListAsync(string datasetID, int? pageNum = null, int? pageSize = null, string? keyword = null, bool? hasCaption = null, CancellationToken cancellationToken = default)
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
            return await context.GetJsonAsync<CozeResult<PhotoInfoList>?>(api, HttpMethod.Get, parameters: parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 调用此接口删除知识库中的文本、图片、表格等文件，支持批量删除。
        /// </summary>
        /// <param name="documentIDs"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult?> DeleteDocumentsAsync(IEnumerable<long> documentIDs, CancellationToken cancellationToken = default)
        {
            var api = "/open_api/knowledge/document/delete";
            var body = new DocumentIdsRequest
            {
                DocumentIds = documentIDs.ToArray()
            };
            return await context.GetJsonAsync<CozeResult>(api, HttpMethod.Post, JsonContent.Create(body, options: context.JsonOptions), cancellationToken: cancellationToken);
        }
    }
}
