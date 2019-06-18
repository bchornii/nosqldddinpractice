namespace NoSqlDddInPractice.Commands.Results
{
    public enum CommandResultStatus
    {
        Ok,
        OkWithId,
        Failed,
        BadCommandData
    }

    public class CommandResult
    {
        private readonly string _message;

        public bool IsSuccess => Status == CommandResultStatus.Ok;
        public bool IsFailed => Status == CommandResultStatus.Failed ||
            Status == CommandResultStatus.BadCommandData;

        public string Id { get; }
        public CommandResultStatus Status { get; }

        public CommandResult(CommandResultStatus commandResultStatus, 
            string message = null, string id = null)
        {
            Id = id;
            _message = message;            
            Status = commandResultStatus;            
        }

        public static CommandResult GetSuccess()
        {
            return new CommandResult(CommandResultStatus.Ok);
        }

        public static CommandResult GetSuccess(string id)
        {
            return new CommandResult(CommandResultStatus.OkWithId, null, id);
        }

        public static CommandResult GetFailed(string message)
        {
            return new CommandResult(CommandResultStatus.Failed, message);
        }
    }
}
