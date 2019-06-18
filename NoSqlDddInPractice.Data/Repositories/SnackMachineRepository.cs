using MongoDB.Bson;
using MongoDB.Driver;
using NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate;
using System.Linq;
using System.Threading.Tasks;

namespace NoSqlDddInPractice.Data.Repositories
{
    public class SnackMachineRepository : ISnackMachineRepository
    {
        private readonly AppDbContext _context;

        public SnackMachineRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SnackMachine> Add(SnackMachine snackMachine)
        {
            InitSlotIds(snackMachine);

            return await _context.SnakMachines
                .InsertOneAsync(snackMachine);
        }

        public async Task<SnackMachine> GetById(string snackMachineId)
        {
            return await _context.SnakMachines.Find(
                sm => sm.Id == snackMachineId).FirstOrDefaultAsync();
        }

        public async Task Update(SnackMachine snackMachine)
        {
            InitSlotIds(snackMachine);

            await _context.SnakMachines.FindOneAndReplaceAsync(
                sm => sm.Id == snackMachine.Id, snackMachine);
        }

        public async Task Remove(SnackMachine snackMachine)
        {
            await _context.SnakMachines.DeleteOneAsync(
                sm => sm.Id == snackMachine.Id, snackMachine);
        }

        private void InitSlotIds(SnackMachine snackMachine)
        {
            if (snackMachine.Slots.Any())
            {
                foreach (var slot in snackMachine.Slots)
                {
                    slot.Id = slot.Id ?? ObjectId.GenerateNewId().ToString();
                }
            }
        }
    }
}
