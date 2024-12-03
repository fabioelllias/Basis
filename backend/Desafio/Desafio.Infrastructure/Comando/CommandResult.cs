namespace Desafio.Infrastructure
{
    public class CommandResult : ICommandResult
    {
        public CommandResult() { }
        public CommandResult(bool success, string? message, object? content)
        {
            Message = message;
            Content = content;
            Success = success;
        }

        public virtual bool Success { get; set; }
        public virtual string Message { get; set; }
        public virtual object? Content { get; set; }

        public CommandResult(IReadOnlyCollection<CommandResult> notifications, object content)
        {
            Success = notifications?.Count == 0;
            Message = notifications != null ? string.Join(",", notifications.Select(item => item.Message).ToArray()) : "";
            Content = content;
        }
    }
}
