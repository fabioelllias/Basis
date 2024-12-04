using Desafio.Infrastructure;
using Flunt.Notifications;
using MediatR;

namespace Desafio.Application
{
    public class LivroObterPorIdQuery : Notifiable<Notification>, IRequest<CommandResult>
    {
        public static LivroObterPorIdQuery Create(int id)
        {
            return new LivroObterPorIdQuery(id);
        }
        public int Id { get; set; }
        public LivroObterPorIdQuery()
        {

        }
        public LivroObterPorIdQuery(int id)
        {
            Id = id;

            AddNotifications(ContractHelper.Create<LivroObterPorIdQuery>(contrato =>
            {
                contrato.IsGreaterThan(Id, 0, "Id", "Id inválido!");
            }));
        }
    }
    public class LivroObterPorIdQueryResult
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public string Autores { get; set; }
        public string Assuntos { get; set; }
    }
}