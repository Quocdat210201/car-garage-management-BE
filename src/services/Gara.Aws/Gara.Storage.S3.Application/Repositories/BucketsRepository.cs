using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Gara.Storage.S3.Domain.Models;
using Gara.Storage.S3.Domain.Repositories;

namespace Gara.Storage.S3.Application.Repositories
{

    public class BucketsRepository : IBucketsRepository
    {
        public readonly IAmazonS3 _s3Client;
        public BucketsRepository(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<CreateBucketsResponse> CreateBucketsResponse(string bucketName)
        {
            var putBucketRequest = new PutBucketRequest
            {
                BucketName = bucketName,
                UseClientRegion = true
            };

            var response = await _s3Client.PutBucketAsync(putBucketRequest);

            return new CreateBucketsResponse
            {
                RequestId = response.ResponseMetadata.RequestId,
                BucketName = bucketName
            };
        }

        public async Task<bool> DoesS3BucketExist(string bucketName)
        {
            return await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
        }
    }
}
