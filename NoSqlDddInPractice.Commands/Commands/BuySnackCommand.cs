using MediatR;
using NoSqlDddInPractice.Commands.Results;

namespace NoSqlDddInPractice.Commands.Commands
{
    public class BuySnackCommand : IRequest<CommandResult>
    {
        public string SnackMachineId { get; set; }
        public int SlotPosition { get; set; }


        public int OneCentCount { get; set; }
        public int TenCentCount { get; set; }
        public int QuarterCount { get; set; }
        public int OneDollarCount { get; set; }
        public int FiveDollarCount { get; set; }

        public string UserId { get; set; }
    }
}
