using NoSqlDddInPractice.Domain.SeedObjects;
using System.Threading.Tasks;

namespace NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate
{
    public interface ISnackMachineRepository : IRepository<SnackMachine>
    {
        void Add(SnackMachine snackMachine);
        Task<SnackMachine> GetById(string snackMachineId);
        void Remove(string snakMachineId);
        void Update(SnackMachine snackMachine);
    }
}
