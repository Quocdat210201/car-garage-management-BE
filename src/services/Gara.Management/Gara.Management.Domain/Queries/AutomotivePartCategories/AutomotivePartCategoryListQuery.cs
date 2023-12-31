using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.AutomotivePartCategories
{
    public class AutomotivePartCategoryListQuery : IRequest<ServiceResult>
    {
    }

    public class AutomotivePartCategoryListQueryHandler : IRequestHandler<AutomotivePartCategoryListQuery, ServiceResult>
    {
        private readonly IRepository<AutomotivePartCategory> _repository;

        public AutomotivePartCategoryListQueryHandler(IRepository<AutomotivePartCategory> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(AutomotivePartCategoryListQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var automotivePartCategories = await _repository.GetAsync();

            result.Success(automotivePartCategories);

            return result;
        }
    }
}
