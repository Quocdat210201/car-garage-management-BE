namespace Gara.Storage.S3.Domain.Models
{
    public class GetBucketsResponse
    {
        public string RequestId { get; set; }

        public string BucketName { get; set; }
    }
}
