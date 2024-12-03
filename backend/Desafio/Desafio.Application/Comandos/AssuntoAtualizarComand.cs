using Desafio.Infrastructure;
using Flunt.Notifications;
using MediatR;

namespace Desafio.Application
{
    public class AssuntoAtualizarComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public AssuntoAtualizarComand(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;

            AddNotifications(ContractHelper.Create<AssuntoAtualizarComand>(contrato =>
            {
                contrato.IsNotNullOrEmpty(Descricao, "Descricao", "Descricao não informada!")
                        .IsTrue(Descricao.Length <= 20, "Descricao", "Tamanho máximo 20 caracteres!")
                        .IsTrue(Descricao.Length >= 2, "Descricao", "Tamanho mínimo 2 caracteres!")
                        .IsGreaterThan(Id, 0, "Id", "Id inválido!");
            }));
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

    }

    public class AssuntoAtualizarResult
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}