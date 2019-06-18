using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NoSqlDddInPractice.Domain.ReadModels.SnackMachine;

namespace NoSqlDddInPractice.Data.Repositories
{
    public class SnackMachineReadRepository : ISnackMachineReadRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SnackMachineReadRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SnackMachineReadModel> GetSnackMachine(string id)
        {
            var snackMachine = await _context.SnakMachines.Find(
                sm => sm.Id == id).FirstOrDefaultAsync();            
            return _mapper.Map<SnackMachineReadModel>(snackMachine);
        }        
    }
}
