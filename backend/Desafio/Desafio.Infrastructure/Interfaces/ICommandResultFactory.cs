namespace Desafio.Infrastructure
{
    public interface ICommandResultFactory
    {
        CommandResult Create(bool success, string message, object data);
        CommandResult Create();
    }
}