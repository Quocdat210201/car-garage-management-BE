namespace Gara.Domain.ServiceResults
{
    public class ServiceResult : BaseServiceResult
    {
        public ServiceResult()
        {
            ErrorMessages = new List<string>();
        }

        public object Data { get; set; }

        public void Success(object data)
        {
            IsSuccess = true;
            Data = data;
        }
    }
}
