using MediatR;
using MongoDB.Bson;
using NoSqlDddInPractice.Commands.Commands;
using NoSqlDddInPractice.Commands.Results;
using NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace DddInPractice.CommandHandlers.Handlers
{
    public class InitializeSnakMachineCommandHandler :
        IRequestHandler<InitializeSnakMachineCommand, CommandResult>
    {
        private readonly ISnackMachineRepository _snakMachineRepository;

        public InitializeSnakMachineCommandHandler(ISnackMachineRepository snakMachineRepository)
        {
            _snakMachineRepository = snakMachineRepository;
        }

        public async Task<CommandResult> Handle(InitializeSnakMachineCommand request, 
            CancellationToken cancellationToken)
        {
            var moneyInside = new Money(request.OneCentCount, request.TenCentCount,
                request.QuarterCount, request.OneDollarCount, request.FiveDollarCount);

            var snackMachine = new SnackMachine(moneyInside);

            var str = snackMachine.ToJson(new MongoDB.Bson.IO.JsonWriterSettings { Indent = true });

            await _snakMachineRepository.Add(snackMachine);
            return CommandResult.GetSuccess();
        }
    }
}
