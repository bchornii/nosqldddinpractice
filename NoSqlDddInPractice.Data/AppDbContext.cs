using MediatR;
using MongoDB.Driver;
using NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate;

namespace NoSqlDddInPractice.Data
{
    public class AppDbContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoDbSet<SnackMachine> SnakMachines { get; }

        public AppDbContext(string connectionString, IMediator mediator)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("snakmachinedb");

            SnakMachines = new MongoDbSet<SnackMachine>(
                _database.GetCollection<SnackMachine>("SnakMachines"), mediator);
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
