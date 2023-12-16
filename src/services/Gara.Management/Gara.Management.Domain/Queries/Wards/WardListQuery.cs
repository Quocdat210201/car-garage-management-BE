using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.Cars
{
    public class WardListQuery : IRequest<ServiceResult>
    {
    }

    public class WardListHandler : IRequestHandler<WardListQuery, ServiceResult>
    {
        private readonly IRepository<Ward> _repository;

        public WardListHandler(IRepository<Ward> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(WardListQuery request, CancellationToken cancellationToken)
        {
            var wardList = await _repository.GetAsync();

            ServiceResult result = new();

            result.Success(wardList);

            return result;
        }
    }
}
