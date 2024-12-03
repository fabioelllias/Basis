using Desafio.Infrastructure;
using Flunt.Notifications;
using MediatR;

namespace Desafio.Application
{
    public class AssuntoObterPorIdQuery : Notifiable<Notification>, IRequest<CommandResult>
    {
        public static AssuntoObterPorIdQuery Create(int id)
        {
            return new AssuntoObterPorIdQuery(id);
        }
        public int Id { get; set; }
        public AssuntoObterPorIdQuery()
        {

        }
        public AssuntoObterPorIdQuery(int id)
        {
            Id = id;

            AddNotifications(ContractHelper.Create<AssuntoObterPorIdQuery>(contrato =>
            {
                contrato.IsGreaterThan(Id, 0, "Id", "Id inválido!");
            }));
        }
    }
    public class AssuntoObterPorIdQueryResult
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}