using MongoDB.Bson.Serialization;
using NoSqlDddInPractice.Domain.SeedObjects;

namespace NoSqlDddInPractice.Data.Configurations
{
    public class EnumerationTypeConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Enumeration>(config =>
            {
                config.AutoMap();
            });
        }
    }
}
