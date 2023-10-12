namespace Gara.Domain.ServiceResults
{
    public class ServiceResultWithData<T>
    {
        public ServiceResultWithData()
        {
            ErrorMessages = new List<string>();
        }

        public bool IsSuccess { get; set; }

        public List<string> ErrorMessages { get; set; }

        public T Data { get; set; }

        public void Success(T data)
        {
            IsSuccess = true;
            Data = data;
        }
    }
}
