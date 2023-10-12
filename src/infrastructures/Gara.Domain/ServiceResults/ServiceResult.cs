namespace Gara.Domain.ServiceResults
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }

        public List<string> ErrorMessages { get; set; }

        public ServiceResult()
        {
            ErrorMessages = new List<string>();
        }
    }
}
