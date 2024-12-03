using Desafio.Infrastructure;
using Flunt.Notifications;
using MediatR;

namespace Desafio.Application
{
    public class AutorObterPorIdQuery : Notifiable<Notification>, IRequest<CommandResult>
    {
        public static AutorObterPorIdQuery Create(int id)
        {
            return new AutorObterPorIdQuery(id);
        }
        public int Id { get; set; }
        public AutorObterPorIdQuery()
        {

        }
        public AutorObterPorIdQuery(int id)
        {
            Id = id;

            AddNotifications(ContractHelper.Create<AutorObterPorIdQuery>(contrato =>
            {
                contrato.IsGreaterThan(Id, 0, "Id", "Id inválido!");
            }));
        }
    }
    public class AutorObterPorIdQueryResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}