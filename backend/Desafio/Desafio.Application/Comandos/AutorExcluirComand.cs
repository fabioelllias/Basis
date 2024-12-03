using Desafio.Infrastructure;
using Flunt.Notifications;
using MediatR;

namespace Desafio.Application
{
    public class AutorExcluirComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public static AutorExcluirComand Create(int id)
        {
            return new AutorExcluirComand(id);
        }
        public AutorExcluirComand(int id)
        {
            Id = id;

            AddNotifications(ContractHelper.Create<AutorCriarComand>(contrato =>
            {
                contrato.IsGreaterThan(Id, 0, "Id", "Id inválido!");
            }));
        }

        public int Id { get; set; }
    }

    public class AutorExcluirResult
    {
    
    }
}