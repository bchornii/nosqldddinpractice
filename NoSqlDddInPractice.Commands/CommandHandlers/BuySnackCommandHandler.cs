using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NoSqlDddInPractice.Commands.Commands;
using NoSqlDddInPractice.Commands.Results;
using NoSqlDddInPractice.Data.Repositories;
using NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate;

namespace DddInPractice.Commands.Handlers
{
    public class BuySnackCommandHandler :
        IRequestHandler<BuySnackCommand, CommandResult>
    {
        private readonly ISnackMachineRepository _snackMachineRepository;

        public BuySnackCommandHandler(
            ISnackMachineRepository snackMachineRepository)
        {
            _snackMachineRepository = snackMachineRepository;
        }

        public async Task<CommandResult> Handle(BuySnackCommand request, 
            CancellationToken cancellationToken)
        {
            var snackMachine = await _snackMachineRepository
                .GetById(request.SnackMachineId);
            if (snackMachine == null)
            {
                return CommandResult.GetFailed("Snack Machine instance is not found.");
            }

            var moneyInTran = new Money(request.OneCentCount, request.TenCentCount,
             request.QuarterCount, request.OneDollarCount, request.FiveDollarCount);

            snackMachine.InsertMoney(moneyInTran);
            if (snackMachine.CanBuySnak(request.SlotPosition))
            {
                snackMachine.BuySnack(request.SlotPosition);

                await _snackMachineRepository.Update(snackMachine);
            }
            else
            {                
                return CommandResult.GetFailed("Cannot buy snack.");
            }

            return CommandResult.GetSuccess();
        }
    }
}
