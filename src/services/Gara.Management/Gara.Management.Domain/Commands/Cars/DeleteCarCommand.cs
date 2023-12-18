using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Commands.Cars
{
    public class DeleteCarCommand : IRequest<ServiceResult>
    {
        public DeleteCarCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class DeleteCarHandler : IRequestHandler<DeleteCarCommand, ServiceResult>
    {
        private readonly IRepository<Car> _repository;

        public DeleteCarHandler(IRepository<Car> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            ServiceResult result = new();
            var currentCar = await _repository.GetByIdAsync(request.Id);

            if (currentCar == null)
            {
                result.IsSuccess = false;
                result.ErrorMessages = new List<string> { "Car not found" };
                return result;
            }

            await _repository.DeleteAsync(currentCar);

            await _repository.SaveChangeAsync();

            result.IsSuccess = true;

            return result;
        }
    }
}
