using MediatR;
using NoSqlDddInPractice.Domain.ReadModels;

namespace NoSqlDddInPractice.Queries.Queries
{
    public class SnackTypesQuery : 
        IRequest<SnakTypeReadModel[]>
    {
    }
}
