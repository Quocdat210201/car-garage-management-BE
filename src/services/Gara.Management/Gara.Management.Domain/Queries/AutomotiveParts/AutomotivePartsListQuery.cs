using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.AutomotiveParts
{
    public class AutomotivePartsListQuery : IRequest<ServiceResult>
    {
    }

    public class AutomotivePartsListQueryHandler : IRequestHandler<AutomotivePartsListQuery, ServiceResult>
    {
        private readonly IRepository<AutomotivePart> _repository;

        public AutomotivePartsListQueryHandler(IRepository<AutomotivePart> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(AutomotivePartsListQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var data = await _repository.GetWithIncludeAsync(null, 0, 0, a => a.AutomotivePartSupplier, a => a.Category);

            result.Success(data);

            return result;
        }
    }
}
