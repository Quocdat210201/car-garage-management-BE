using Gara.Storage.S3.Domain.Models.Files;
using Microsoft.AspNetCore.Http;

namespace Gara.Storage.S3.Domain.Repositories
{
    public interface IFilesRepository
    {
        //Task<IEnumerable<File>> GetFilesAsync(string bucketName, string prefix, string delimiter = null);
        //Task<File> GetFileAsync(string bucketName, string key);
        //Task<File> CreateFileAsync(string bucketName, string key, string contentType, byte[] content);
        Task<CreateFilesResponse> UpdateFileAsync(string bucketName, IList<IFormFile> files);
        //Task DeleteFileAsync(string bucketName, string key);
    }
}
