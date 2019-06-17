using FluentValidation;
using NoSqlDddInPractice.Commands.Commands;

namespace NoSqlDddInPractice.Api.Infrastructure.Validators
{
    public class InitializeSnackMachineCommandValidator :
       AbstractValidator<InitializeSnakMachineCommand>
    {
        public InitializeSnackMachineCommandValidator()
        {
            RuleFor(m => m.OneCentCount + m.TenCentCount +
                         m.QuarterCount + m.OneDollarCount +
                         m.FiveDollarCount)
                .NotEmpty()
                .WithMessage("Please initialize machine with some money.");
        }
    }
}
