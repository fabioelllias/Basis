using Desafio.Infrastructure;
using Flunt.Notifications;
using MediatR;

namespace Desafio.Application
{
    public class AssuntoExcluirComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public static AssuntoExcluirComand Create(int id)
        {
            return new AssuntoExcluirComand(id);
        }
        public AssuntoExcluirComand(int id)
        {
            Id = id;

            AddNotifications(ContractHelper.Create<AssuntoExcluirComand>(contrato =>
            {
                contrato.IsGreaterThan(Id, 0, "Id", "Id inválido!");
            }));
        }

        public int Id { get; set; }
    }

    public class AssuntoExcluirResult
    {
    
    }
}