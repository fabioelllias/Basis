using Desafio.Core.Entidades;
using Desafio.Infrastructure;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace Desafio.Application
{
    public class LivroCriarComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public LivroCriarComand(string titulo, string editora, int edicao, string anoPublicacao, int[] autores, int[] assuntos)
        {
            Titulo = titulo;
            Editora = editora;
            Edicao = edicao;
            AnoPublicacao = anoPublicacao;
            Autores = autores;
            Assuntos = assuntos;

            AddNotifications(ContractHelper.Create<LivroAtualizarComand>(contrato =>
            {
                contrato
                        .IsGreaterThan(Edicao, 0, "Edicao", "Edicao inválida!")
                        .IsNotNullOrEmpty(Titulo, "Titulo", "Titulo não informado!")
                        .IsTrue(Titulo.Length <= 40, "Titulo", "Tamanho máximo 40 caracteres!")
                        .IsTrue(Titulo.Length >= 2, "Titulo", "Tamanho mínimo 2 caracteres!")
                        .IsNotNullOrEmpty(Editora, "Editora", "Editora não informada!")
                        .IsTrue(Editora.Length <= 40, "Editora", "Tamanho máximo 40 caracteres!")
                        .IsTrue(Editora.Length >= 2, "Editora", "Tamanho mínimo 2 caracteres!")
                        .IsTrue(int.TryParse(AnoPublicacao, out int anoValido) && AnoPublicacao.Length == 4 && anoValido > 1 && anoValido <= DateTime.Now.Year,
                            "AnoPublicacao", "Ano de publicação inválido!")
                        .IsTrue(autores.Length > 0, "Autores", "Autor(es) não informado(s)!")
                        .IsTrue(assuntos.Length > 0, "Assuntos", "Assunto(s) não informado(s)!");

            }));
        }

        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public int[] Autores { get; set; }
        public int[] Assuntos { get; set; }

    }

    public class LivroCriarResult
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