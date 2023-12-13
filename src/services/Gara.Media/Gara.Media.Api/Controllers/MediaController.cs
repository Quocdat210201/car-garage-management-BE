using Gara.Media.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Media.Api.Controllers
{
    [Route("api/media")]
    public class MediaController : Controller
    {
        private readonly ILogger<MediaController> _logger;

        public MediaController(ILogger<MediaController> logger)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(string folder, string fileName, [FromForm] IFormFile file)
        {
            _logger.LogInformation($"Uploading the {fileName} to {folder}");
            var t1 = Task.Run(() => FileHelper.SaveFile(folder, fileName, file));

            await Task.WhenAll(t1);

            var extension = Path.GetExtension(file.FileName);

            return Ok(Url.Action("GetFile", "Media", new { folder = folder, fileName = string.Format("{0}{1}", fileName, extension) }, "https", "localhost:5001"));
        }

        [HttpGet("{folder}/{fileName}")]
        public async Task<IActionResult> GetFile(string folder, string fileName)
        {
            var t1 = Task.Run(() => FileHelper.GetFile(folder, fileName));

            await Task.WhenAll(t1);

            var fileStream = System.IO.File.OpenRead(t1.Result);

            var contentType = GetMimeType(fileName);

            return base.File(fileStream, contentType);
        }

        private string GetMimeType(string fileName)
        {
            return MimeTypes.GetMimeType(fileName);
        }
    }
}