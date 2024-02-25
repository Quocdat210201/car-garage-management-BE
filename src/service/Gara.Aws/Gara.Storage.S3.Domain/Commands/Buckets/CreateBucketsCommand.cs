using Gara.Domain.ServiceResults;
using Gara.Storage.S3.Domain.Repositories;
using MediatR;

namespace Gara.Storage.S3.Domain.Commands.Buckets
{
    public class CreateBucketsCommand : IRequest<ServiceResult>
    {
        public string BucketName { get; set; }
    }

    public class CreateBucketsHandler : IRequestHandler<CreateBucketsCommand, ServiceResult>
    {
        public IBucketsRepository _bucketsRepository;
        public CreateBucketsHandler(IBucketsRepository bucketsRepository)
        {
            _bucketsRepository = bucketsRepository;
        }

        public async Task<ServiceResult> Handle(CreateBucketsCommand request, CancellationToken cancellationToken)
        {
            ServiceResult result = new();
            if (await _bucketsRepository.DoesS3BucketExist(request.BucketName))
            {
                result.ErrorMessages = new List<string>() { "Bucket already exists" };
                return result;
            }

            var response = await _bucketsRepository.CreateBucketsResponse(request.BucketName);
            if (response == null)
            {
                result.ErrorMessages = new List<string>() { "Bucket creation failed" };
                result.IsSuccess = false;
                return result;
            }
            result.Data = response;
            result.IsSuccess = true;

            return result;
        }
    }
}
