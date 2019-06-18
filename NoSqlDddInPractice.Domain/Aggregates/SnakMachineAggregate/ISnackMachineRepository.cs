using NoSqlDddInPractice.Domain.SeedObjects;
using System.Threading.Tasks;

namespace NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate
{
    public interface ISnackMachineRepository : IRepository<SnackMachine>
    {
        Task<SnackMachine> Add(SnackMachine snackMachine);
        Task<SnackMachine> GetById(string snackMachineId);
        Task Remove(SnackMachine snackMachine);
        Task Update(SnackMachine snackMachine);
    }
}
