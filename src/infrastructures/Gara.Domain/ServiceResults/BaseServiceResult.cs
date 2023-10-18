using System.Net;

namespace Gara.Domain.ServiceResults
{
    public class BaseServiceResult
    {
        public bool IsSuccess { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public List<string> ErrorMessages { get; set; }

        public BaseServiceResult()
        {
            ErrorMessages = new List<string>();
        }
    }
}
