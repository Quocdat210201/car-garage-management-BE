using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Gara.Management.Domain.Queries.AutomotiveParts
{
    public class AutomotivePartsByCategoryAndSupplierQuery : IRequest<ServiceResult>
    {
        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid AutomotivePartSupplierId { get; set; }
    }

    public class AutomotivePartsByCategoryAndSupplierHandler : IRequestHandler<AutomotivePartsByCategoryAndSupplierQuery, ServiceResult>
    {
        private readonly IRepository<AutomotivePart> _repository;

        public AutomotivePartsByCategoryAndSupplierHandler(IRepository<AutomotivePart> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(AutomotivePartsByCategoryAndSupplierQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var data = await _repository.GetWithIncludeAsync(p => p.CategoryId == request.CategoryId
                        && p.AutomotivePartSupplierId == request.AutomotivePartSupplierId,
                        0, 0);

            result.Success(data);
            return result;
        }
    }
}
