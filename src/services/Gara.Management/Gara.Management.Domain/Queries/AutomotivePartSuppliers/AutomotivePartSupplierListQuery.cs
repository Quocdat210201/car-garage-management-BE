using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.AutomotivePartSuppliers
{
    public class AutomotivePartSupplierListQuery : IRequest<ServiceResult>
    {
    }

    public class AutomotivePartSupplierListQueryHandler : IRequestHandler<AutomotivePartSupplierListQuery, ServiceResult>
    {
        private readonly IRepository<AutomotivePartSupplier> _repository;

        public AutomotivePartSupplierListQueryHandler(IRepository<AutomotivePartSupplier> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(AutomotivePartSupplierListQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var automotivePartSuppliers = await _repository.GetAsync();

            result.Success(automotivePartSuppliers);

            return result;
        }
    }
}
