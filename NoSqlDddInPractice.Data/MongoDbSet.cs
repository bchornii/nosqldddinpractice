using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using NoSqlDddInPractice.Domain.SeedObjects;

namespace NoSqlDddInPractice.Data
{
    public class MongoDbSet<TDocument> where TDocument : Entity
    {
        private readonly IMediator _mediator;
        private readonly IMongoCollection<TDocument> _collection;
        
        public MongoDbSet(IMongoCollection<TDocument> collection, IMediator mediator)
        {
            _collection = collection;
            _mediator = mediator;
        }

        public IFindFluent<TDocument, TDocument> Find(Expression<Func<TDocument, bool>> filter, 
            FindOptions options = null)
        {
            return _collection.Find(filter, options);
        }

        public async Task<TDocument> InsertOneAsync(TDocument document, InsertOneOptions options = null, 
            CancellationToken cancellationToken = default)
        {
            await _collection.InsertOneAsync(document, options, cancellationToken);
            await PublishDomainEvents(document);
            return document;
        }

        public async Task<TDocument> FindOneAndReplaceAsync(Expression<Func<TDocument, bool>> filter, 
            TDocument replacement, FindOneAndReplaceOptions<TDocument, TDocument> options = null, 
            CancellationToken cancellationToken = default)
        {
            var result = await _collection.FindOneAndReplaceAsync(filter, replacement, options, cancellationToken);
            await PublishDomainEvents(replacement);
            return result;
        }

        public async Task<DeleteResult> DeleteOneAsync(Expression<Func<TDocument, bool>> filter, TDocument document,
            CancellationToken cancellationToken = default)
        {
            var result = await _collection.DeleteOneAsync(filter, cancellationToken);
            await PublishDomainEvents(document);
            return result;
        }        

        private async Task PublishDomainEvents(TDocument document)
        {
            var domainEvents = document.DomainEvents ?? 
                Array.Empty<INotification>();

            var tasks = domainEvents
              .Select(async (domainEvent) => {
                  await _mediator.Publish(domainEvent);
              });

            await Task.WhenAll(tasks);
        }
    }
}
