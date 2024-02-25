using Gara.Storage.S3.Domain.Models;

namespace Gara.Storage.S3.Domain.Repositories
{
    public interface IBucketsRepository
    {
        Task<bool> DoesS3BucketExist(string bucketName);

        Task<CreateBucketsResponse> CreateBucketsResponse(string bucketName);
    }
}
