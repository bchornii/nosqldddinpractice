using System.Threading.Tasks;

namespace NoSqlDddInPractice.Domain.ReadModels.SnackMachine
{
    public interface ISnackMachineReadRepository
    {
        Task<SnackMachineReadModel> GetSnackMachine(string id);
    }
}
