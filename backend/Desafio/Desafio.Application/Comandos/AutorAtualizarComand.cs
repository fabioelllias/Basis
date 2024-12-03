using Desafio.Infrastructure;
using Flunt.Notifications;
using MediatR;

namespace Desafio.Application
{
    public class AutorAtualizarComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public AutorAtualizarComand(int id, string nome)
        {
            Id = id;
            Nome = nome;

            AddNotifications(ContractHelper.Create<AutorCriarComand>(contrato =>
            {
                contrato.IsNotNullOrEmpty(Nome, "Nome", "Nome não informado!")
                        .IsTrue(Nome.Length <= 40, "Nome", "Tamanho máximo 40 caracteres!")
                        .IsTrue(Nome.Length >= 2, "Nome", "Tamanho mínimo 2 caracteres!")
                        .IsGreaterThan(Id, 0, "Id", "Id inválido!");
            }));
        }

        public int Id { get; set; }
        public string Nome { get; set; }

    }

    public class AutorAtualizarResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}