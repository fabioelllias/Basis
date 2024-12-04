using Desafio.Infrastructure;
using Flunt.Notifications;
using MediatR;

namespace Desafio.Application
{
    public class LivroExcluirComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public static LivroExcluirComand Create(int id)
        {
            return new LivroExcluirComand(id);
        }
        public LivroExcluirComand(int id)
        {
            Id = id;

            AddNotifications(ContractHelper.Create<LivroExcluirComand>(contrato =>
            {
                contrato.IsGreaterThan(Id, 0, "Id", "Id inválido!");
            }));
        }

        public int Id { get; set; }
    }

    public class LivroExcluirResult
    {
    
    }
}