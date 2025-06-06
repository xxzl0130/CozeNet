﻿using CozeNet.Core;
using CozeNet.Core.Models;
using CozeNet.File.Models;

namespace CozeNet.File
{
    public class FileService(Context context)
    {
        /// <summary>
        /// 消息中无法直接使用本地文件，创建消息或对话前，需要先调用此接口上传本地文件到扣子。
        /// 上传文件后，你可以在消息中通过指定 file_id 的方式在多模态内容中直接使用此文件。使用方式可参考object_string object。
        /// </summary>
        /// <param name="localFilename"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult<FileObject>?> UploadFileAsync(string localFilename, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(localFilename) || !System.IO.File.Exists(localFilename))
                return null;
            await using var fileStream = System.IO.File.OpenRead(localFilename);
            return await UploadFileAsync(fileStream, Path.GetFileName(localFilename), cancellationToken);
        }

        /// <summary>
        /// 消息中无法直接使用本地文件，创建消息或对话前，需要先调用此接口上传本地文件到扣子。
        /// 上传文件后，你可以在消息中通过指定 file_id 的方式在多模态内容中直接使用此文件。使用方式可参考object_string object。
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filename"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult<FileObject>?> UploadFileAsync(Stream stream, string filename, CancellationToken cancellationToken = default)
        {
            using var multipartContent = new MultipartFormDataContent();
            using var fileContent = new StreamContent(stream);
            multipartContent.Add(fileContent, "file", filename);
            const string api = "/v1/files/upload";
            return await context.GetJsonAsync<CozeResult<FileObject>>(api, HttpMethod.Post, multipartContent, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 查看已上传的文件详情。
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult<FileObject>?> GetFileInfoAsync(string fileId, CancellationToken cancellationToken = default)
        {
            const string api = "/v1/files/retrieve";
            return await context.GetJsonAsync<CozeResult<FileObject>>(api, HttpMethod.Get, parameters: new Dictionary<string, string> { { "file_id", fileId } }, cancellationToken: cancellationToken);
        }
    }
}
