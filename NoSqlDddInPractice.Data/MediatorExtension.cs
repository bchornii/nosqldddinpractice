using MediatR;
using MongoDB.Driver;
using NoSqlDddInPractice.Domain.SeedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NoSqlDddInPractice.Data
{
    internal static class MediatorExtension
    {
        public static async Task DispatchDomainEvents(this IMediator mediator, 
            AppDbContext context)
        {
            var domainEntities = GetInstanceField(
                typeof(AppDbContext), context, "_touchedEntities") as List<Entity>;

            var domainEvents = domainEntities
                .SelectMany(e => e.DomainEvents ?? Array.Empty<INotification>())
                .ToList();

            domainEntities.ToList()
               .ForEach(entity => entity.ClearDomainEvents());

            var tasks = domainEvents
              .Select(async (domainEvent) => {
                  await mediator.Publish(domainEvent);
              });

            await Task.WhenAll(tasks);
        }

        internal static object GetInstanceField(Type type, object instance, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }
    }
}
