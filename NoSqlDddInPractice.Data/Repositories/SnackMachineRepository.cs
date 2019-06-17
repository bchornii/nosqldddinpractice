using MongoDB.Bson;
using MongoDB.Driver;
using NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate;
using NoSqlDddInPractice.Domain.SeedObjects;
using System.Linq;
using System.Threading.Tasks;

namespace NoSqlDddInPractice.Data.Repositories
{
    public class SnackMachineRepository : ISnackMachineRepository
    {
        private readonly AppDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public SnackMachineRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(SnackMachine snackMachine)
        {
            if (snackMachine.Slots.Any())
            {
                foreach (var slot in snackMachine.Slots)
                {
                    slot.Id = slot.Id ?? ObjectId.GenerateNewId().ToString();
                }
            }
            _context.AddCommand(
                document: snackMachine,
                command: () => _context.SnakMachines.InsertOneAsync(snackMachine));
        }

        public async Task<SnackMachine> GetById(string snackMachineId)
        {
            return await _context.SnakMachines.Find(
                sm => sm.Id == snackMachineId).FirstOrDefaultAsync();
        }

        public void Update(SnackMachine snackMachine)
        {
            if (snackMachine.Slots.Any())
            {
                foreach (var slot in snackMachine.Slots)
                {
                    slot.Id = slot.Id ?? ObjectId.GenerateNewId().ToString();
                }
            }
            _context.AddCommand(
                document: snackMachine,
                command: () => _context.SnakMachines.FindOneAndReplaceAsync(sm => sm.Id == snackMachine.Id, snackMachine));
        }

        public void Remove(string snakMachineId)
        {
            _context.AddCommand(() =>
                _context.SnakMachines.DeleteOneAsync(sm => sm.Id == snakMachineId));
        }
    }
}
