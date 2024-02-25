namespace Gara.Storage.S3.Domain.Models.Files
{
    public class CreateFilesResponse
    {
        public IList<string> PreSignedUrl { get; set; }
    }
}