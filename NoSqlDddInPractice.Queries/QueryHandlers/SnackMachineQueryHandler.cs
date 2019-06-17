using MediatR;
using NoSqlDddInPractice.Domain.ReadModels.SnackMachine;
using NoSqlDddInPractice.Queries.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace NoSqlDddInPractice.Queries.QueryHandlers
{
    public class SnackMachineQueryHandler : IRequestHandler<
        SnackMachineQuery, SnackMachineReadModel>
    {
        private readonly ISnackMachineReadRepository _repository;

        public SnackMachineQueryHandler(ISnackMachineReadRepository repository)
        {
            _repository = repository;
        }

        public Task<SnackMachineReadModel> Handle(
            SnackMachineQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetSnackMachine(request.Id);
        }
    }
}
