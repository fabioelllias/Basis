using Desafio.Infrastructure;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace Desafio.Application
{
    public class AutorCriarComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public AutorCriarComand(string nome)
        {
            Nome = nome;

            AddNotifications(ContractHelper.Create<AutorCriarComand>(contrato =>
            {
                contrato.IsNotNullOrEmpty(Nome, "Nome", "Nome não informado!")
                        .IsTrue(Nome.Length <= 40, "Nome", "Tamanho máximo 40 caracteres!")
                        .IsTrue(Nome.Length >= 2, "Nome", "Tamanho mínimo 2 caracteres!");
            }));
        }

        public string Nome { get; set; }

    }

    public class AutorCriarResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}