using Desafio.Infrastructure;

public class CommandResultFactory : ICommandResultFactory
{
    public CommandResult Create(bool success, string message, object data)
    {
        return new CommandResult(success, message, data);
    }
}