using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NoSqlDddInPractice.Commands.Commands;
using NoSqlDddInPractice.Commands.Results;
using NoSqlDddInPractice.Domain.Aggregates.SnakMachineAggregate;

namespace DddInPractice.CommandHandlers.Handlers
{
    public class AddSnackMachineSlotCommandHandler :
        IRequestHandler<AddSnackMachineSlotCommand, CommandResult>
    {
        private readonly ISnackMachineRepository _snackMachineRepository;

        public AddSnackMachineSlotCommandHandler(
            ISnackMachineRepository snackMachineRepository)
        {
            _snackMachineRepository = snackMachineRepository;
        }

        public async Task<CommandResult> Handle(AddSnackMachineSlotCommand request, 
            CancellationToken cancellationToken)
        {
            var snackMachine = await _snackMachineRepository
                .GetById(request.SnackMachineId);
            if(snackMachine == null)
            {
                return CommandResult.GetFailed("Snack Machine instance is not found.");
            }

            if (snackMachine.CanAddSlot())
            {
                snackMachine.AddSlot(request.Position, request.ItemsQuantity,
                            request.ItemPrice, request.SnackTypeId);

                await _snackMachineRepository.Update(snackMachine);
                return CommandResult.GetSuccess();
            }
            else
            {
                return CommandResult.GetFailed("No available place for slot");
            }            
        }
    }
}
