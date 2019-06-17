using System;
using System.Threading.Tasks;

namespace NoSqlDddInPractice.Domain.SeedObjects
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
