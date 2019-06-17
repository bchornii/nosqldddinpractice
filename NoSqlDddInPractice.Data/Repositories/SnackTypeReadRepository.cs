using System.Linq;
using System.Threading.Tasks;
using NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate;
using NoSqlDddInPractice.Domain.ReadModels;
using NoSqlDddInPractice.Domain.SeedObjects;

namespace NoSqlDddInPractice.Data.Repositories
{
    public class SnackTypeReadRepository : ISnackTypeReadRepository
    {
        public Task<SnakTypeReadModel[]> GetSnakTypes()
        {
            var result = Enumeration.GetAll<SnackType>()
                .Select(t => new SnakTypeReadModel
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToArray();
            return Task.FromResult(result);
        }
    }
}
