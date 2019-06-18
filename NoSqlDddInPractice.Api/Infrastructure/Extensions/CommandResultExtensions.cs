using Microsoft.AspNetCore.Mvc;
using NoSqlDddInPractice.Commands.Results;

namespace NoSqlDddInPractice.Api.Infrastructure.Extensions
{
    public static class CommandResultExtensions
    {
        public static IActionResult ToHttpActionResult(this CommandResult commandResult)
        {
            return commandResult.Status switch
            {                
                CommandResultStatus.Ok => new OkResult(),
                CommandResultStatus.OkWithId => new OkObjectResult(commandResult.Id),
                CommandResultStatus.Failed => new BadRequestResult(),
                CommandResultStatus.BadCommandData => new BadRequestResult(),
                _ => new NoContentResult() as IActionResult
            };
        }
    }
}
