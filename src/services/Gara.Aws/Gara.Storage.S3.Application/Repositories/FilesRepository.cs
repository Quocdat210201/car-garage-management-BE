using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Gara.Storage.S3.Domain.Models.Files;
using Gara.Storage.S3.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Gara.Storage.S3.Application.Repositories
{
    public class FilesRepository : IFilesRepository
    {
        public readonly IAmazonS3 _s3Client;

        public FilesRepository(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<CreateFilesResponse> UpdateFileAsync(string bucketName, IList<IFormFile> files)
        {
            var preSignedUrls = new List<string>();

            foreach (var file in files)
            {
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = file.OpenReadStream(),
                    Key = file.FileName,
                    BucketName = bucketName,
                    CannedACL = S3CannedACL.NoACL
                };

                using (var transferUtility = new TransferUtility(_s3Client))
                {
                    await transferUtility.UploadAsync(uploadRequest);
                }

                var presignedUrlRequest = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = file.FileName,
                    Expires = DateTime.Now.AddHours(1)
                };

                var url = _s3Client.GetPreSignedURL(presignedUrlRequest);

                preSignedUrls.Add(url);
            }

            return new CreateFilesResponse
            {
                PreSignedUrl = preSignedUrls
            };
        }
    }
}
