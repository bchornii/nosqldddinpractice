using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate;

namespace NoSqlDddInPractice.Data.Configurations
{

    public class SnackMachineTypeConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<SnackMachine>(config =>
            {
                config.AutoMap();
                config.SetIgnoreExtraElements(true);

                config.UnmapMember(sm => sm.MoneyInTransaction);

                config.MapMember(sm => sm.MoneyInside).SetElementName("MoneyInside");                
                config.MapField("_slots").SetElementName("Slots");
            });

            BsonClassMap.RegisterClassMap<Slot>(config =>
            {
                config.AutoMap();
                config.SetIgnoreExtraElements(true);

                config.MapField("_position").SetElementName("Position");
                config.MapField("_itemsQuantity").SetElementName("ItemsQuantity");
                config.MapField("_itemPrice").SetElementName("ItemPrice")
                      .SetSerializer(new DecimalSerializer(BsonType.Decimal128));
            });
        }
    }
}
