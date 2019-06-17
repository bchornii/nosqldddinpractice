using MediatR;
using NoSqlDddInPractice.Commands.Results;

namespace NoSqlDddInPractice.Commands.Commands
{
    public class AddSnackMachineSlotCommand : IRequest<CommandResult>
    {
        public string SnackMachineId { get; set; }

        public int Position { get; set; }
        public int ItemsQuantity { get; set; }
        public decimal ItemPrice { get; set; }
        public int SnackTypeId { get; set; }

        public string UserId { get; set; }
    }
}
