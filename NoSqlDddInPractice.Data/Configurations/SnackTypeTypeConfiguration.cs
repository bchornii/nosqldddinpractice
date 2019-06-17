using MongoDB.Bson.Serialization;
using NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate;

namespace NoSqlDddInPractice.Data.Configurations
{
    public class SnackTypeTypeConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<SnackType>(config =>
            {
            });
        }
    }
}
