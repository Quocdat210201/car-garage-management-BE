using Gara.Domain.ServiceResults;
using Gara.Storage.S3.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Gara.Storage.S3.Domain.Commands.Files
{
    public class CreateFilesCommand : IRequest<ServiceResult>
    {
        public string BucketsName { get; set; }

        public IList<IFormFile> FormFile { get; set; }
    }

    public class CreateFilesHandler : IRequestHandler<CreateFilesCommand, ServiceResult>
    {
        public IFilesRepository _filesRepository;

        public CreateFilesHandler(IFilesRepository filesRepository)
        {
            _filesRepository = filesRepository;
        }

        public async Task<ServiceResult> Handle(CreateFilesCommand request, CancellationToken cancellationToken)
        {
            ServiceResult result = new();

            var response = await _filesRepository.UpdateFileAsync(request.BucketsName, request.FormFile);

            result.Data = response;
            result.IsSuccess = true;

            return result;
        }
    }
}
