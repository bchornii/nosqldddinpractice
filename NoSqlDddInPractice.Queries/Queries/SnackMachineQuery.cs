using MediatR;
using NoSqlDddInPractice.Domain.ReadModels.SnackMachine;

namespace NoSqlDddInPractice.Queries.Queries
{
    public class SnackMachineQuery : IRequest<SnackMachineReadModel>
    {
        public string Id { get; set; }
    }
}
