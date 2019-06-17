using System.Threading.Tasks;

namespace NoSqlDddInPractice.Domain.ReadModels
{
    public interface ISnackTypeReadRepository
    {
        Task<SnakTypeReadModel[]> GetSnakTypes();
    }
}
