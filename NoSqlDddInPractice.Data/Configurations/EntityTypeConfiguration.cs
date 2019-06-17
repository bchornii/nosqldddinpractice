using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using NoSqlDddInPractice.Domain.SeedObjects;

namespace NoSqlDddInPractice.Data.Configurations
{
    public class EntityTypeConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Entity>(config =>
            {
                config.MapIdMember(m => m.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                config.IdMemberMap
                      .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }
    }
}
