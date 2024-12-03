using Desafio.Infrastructure;
using Flunt.Notifications;
using MediatR;

namespace Desafio.Application
{
    public class AssuntoCriarComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public AssuntoCriarComand(string descricao)
        {
            Descricao = descricao;

            AddNotifications(ContractHelper.Create<AssuntoCriarComand>(contrato =>
            {
                contrato.IsNotNullOrEmpty(Descricao, "Descricao", "Descricao não informada!")
                        .IsTrue(Descricao.Length <= 40, "Descricao", "Tamanho máximo 20 caracteres!")
                        .IsTrue(Descricao.Length >= 2, "Descricao", "Tamanho mínimo 2 caracteres!");
            }));
        }

        public string Descricao { get; set; }

    }

    public class AssuntoCriarResult
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}