using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NoSqlDddInPractice.Domain.ReadModels;
using NoSqlDddInPractice.Queries.Queries;

namespace NoSqlDddInPractice.Queries.QueryHandlers
{
    public class SnackTypesQueryHandler : IRequestHandler<
        SnackTypesQuery, SnakTypeReadModel[]>
    {
        private readonly ISnackTypeReadRepository _snakTypeReadRespository;

        public SnackTypesQueryHandler(ISnackTypeReadRepository snakTypeReadRespository)
        {
            _snakTypeReadRespository = snakTypeReadRespository;
        }

        public Task<SnakTypeReadModel[]> Handle(SnackTypesQuery request, 
            CancellationToken cancellationToken)
        {
            return _snakTypeReadRespository.GetSnakTypes();
        }
    }
}
