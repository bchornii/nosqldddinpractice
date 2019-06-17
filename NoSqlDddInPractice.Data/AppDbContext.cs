using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate;
using NoSqlDddInPractice.Domain.SeedObjects;

namespace NoSqlDddInPractice.Data
{
    public class AppDbContext : IUnitOfWork
    {
        private readonly List<Entity> _touchedEntities;        
        private readonly List<Func<Task>> _commands;

        private IMongoClient Client { get; set; }

        private const string _databaseName = "snakmachinedb";
        private readonly IMediator _mediator;

        private IMongoDatabase Database { get; set; }
        
        public IMongoCollection<SnackMachine> SnakMachines =>
            Database.GetCollection<SnackMachine>("SnakMachines");

        public AppDbContext(string connectionString, IMediator mediator)
        {
            _mediator = mediator;
            _touchedEntities = new List<Entity>();
            _commands = new List<Func<Task>>();         

            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(_databaseName);            
        }

        public async Task<bool> Commit()
        {
            // TODO: domain events can be saved before entities
            // for case of some exception during entity saving

            var commandTasks = _commands.Select(c => c());
            await Task.WhenAll(commandTasks);
            _commands.Clear();

            await _mediator.DispatchDomainEvents(this);
            _touchedEntities.Clear();

            return true;
        }

        public void AddCommand(Func<Task> command)
        {
            _commands.Add(command);
        }

        public void AddCommand<TDocument>(TDocument document, 
            Func<Task> command) where TDocument : Entity
        {
            _touchedEntities.Add(document);
            _commands.Add(command);
        }

        /// This method wraps changes to domain entities into
        /// single transaction. BUT as far as we need per aggregate
        /// transaction boundaries and we need to save aggregate as
        /// single document (and mongo guarantees per doc atomical tran)
        /// we can ommit such manual transaction because btw aggregates 
        /// we can rely on eventual consistency.
        /// https://www.infoq.com/news/2014/12/aggregates-ddd/
        //public async Task<bool> Commit()
        //{            
        //    using (var session = Client.StartSession())
        //    {
        //        session.StartTransaction(new TransactionOptions(
        //            readConcern: ReadConcern.Snapshot,
        //            writeConcern: WriteConcern.WMajority));

        //        var commitRetriesCounter = 0;
        //        while(commitRetriesCounter <= 5)
        //        {
        //            try
        //            {
        //                var commandTasks = _commands.Select(c => c());
        //                await Task.WhenAll(commandTasks);

        //                await session.CommitTransactionAsync();
        //                _commands.Clear();
        //                return true;
        //            }
        //            catch (MongoException exception)
        //            {
        //                if (exception.HasErrorLabel("UnknownTransactionCommitResult"))
        //                {
        //                    if (commitRetriesCounter < 5)
        //                    {
        //                        commitRetriesCounter++;
        //                        continue;
        //                    }
        //                    else
        //                    {
        //                        throw;
        //                    }
        //                }
        //                else
        //                {
        //                    await session.AbortTransactionAsync();
        //                    throw;
        //                }                        
        //            }
        //        }
        //    }

        //    return false;
        //}            
    }
}
