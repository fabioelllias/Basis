using Desafio.Application;
using FluentAssertions;

namespace Desafio.UnitTest;

[TestFixture]
public class LivroAtualizarComandTests
{
    [Test]
    public void Deve_Ser_Valido_Quando_Dados_Sao_Validos()
    {
        // Arrange
        var comand = new LivroAtualizarComand(
            id: 1,
            titulo: "Título válido",
            editora: "Editora válida",
            edicao: 1,
            anoPublicacao: "2023",
            autores: new[] { 1, 2 },
            assuntos: new[] { 10, 20 }
        );

        // Assert
        comand.IsValid.Should().BeTrue();
        comand.Notifications.Should().BeEmpty();
    }

    [Test]
    public void Deve_Ser_Invalido_Quando_Id_For_Invalido()
    {
        // Arrange
        var comand = new LivroAtualizarComand(
            id: 0,
            titulo: "Título válido",
            editora: "Editora válida",
            edicao: 1,
            anoPublicacao: "2023",
            autores: new[] { 1, 2 },
            assuntos: new[] { 10, 20 }
        );

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "Id" && n.Message == "Id inválido!");
    }

    [TestCase("", "Titulo não informado!")]
    [TestCase("A", "Tamanho mínimo 2 caracteres!")]
    [TestCase("Título muito longo que excede o limite de 40 caracteres", "Tamanho máximo 40 caracteres!")]
    public void Deve_Ser_Invalido_Quando_Titulo_For_Invalido(string titulo, string mensagemEsperada)
    {
        // Arrange
        var comand = new LivroAtualizarComand(
            id: 1,
            titulo: titulo,
            editora: "Editora válida",
            edicao: 1,
            anoPublicacao: "2023",
            autores: new[] { 1, 2 },
            assuntos: new[] { 10, 20 }
        );

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "Titulo" && n.Message == mensagemEsperada);
    }

    [TestCase("", "Editora não informada!")]
    [TestCase("A", "Tamanho mínimo 2 caracteres!")]
    [TestCase("Editora muito longa que excede o limite de 40 caracteres", "Tamanho máximo 40 caracteres!")]
    public void Deve_Ser_Invalido_Quando_Editora_For_Invalida(string editora, string mensagemEsperada)
    {
        // Arrange
        var comand = new LivroAtualizarComand(
            id: 1,
            titulo: "Título válido",
            editora: editora,
            edicao: 1,
            anoPublicacao: "2023",
            autores: new[] { 1, 2 },
            assuntos: new[] { 10, 20 }
        );

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "Editora" && n.Message == mensagemEsperada);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void Deve_Ser_Invalido_Quando_Edicao_For_Invalida(int edicao)
    {
        // Arrange
        var comand = new LivroAtualizarComand(
            id: 1,
            titulo: "Título válido",
            editora: "Editora válida",
            edicao: edicao,
            anoPublicacao: "2023",
            autores: new[] { 1, 2 },
            assuntos: new[] { 10, 20 }
        );

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "Edicao" && n.Message == "Edicao inválida!");
    }

    [TestCase("20235", "Ano de publicação inválido!")]
    [TestCase("abcd", "Ano de publicação inválido!")]
    [TestCase("2050", "Ano de publicação inválido!")]
    public void Deve_Ser_Invalido_Quando_AnoPublicacao_For_Invalido(string anoPublicacao, string mensagemEsperada)
    {
        // Arrange
        var comand = new LivroAtualizarComand(
            id: 1,
            titulo: "Título válido",
            editora: "Editora válida",
            edicao: 1,
            anoPublicacao: anoPublicacao,
            autores: new[] { 1, 2 },
            assuntos: new[] { 10, 20 }
        );

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "AnoPublicacao" && n.Message == mensagemEsperada);
    }

    [Test]
    public void Deve_Ser_Invalido_Quando_Nao_Houver_Autores()
    {
        // Arrange
        var comand = new LivroAtualizarComand(
            id: 1,
            titulo: "Título válido",
            editora: "Editora válida",
            edicao: 1,
            anoPublicacao: "2023",
            autores: Array.Empty<int>(),
            assuntos: new[] { 10, 20 }
        );

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "Autores" && n.Message == "Autor(es) não informado(s)!");
    }

    [Test]
    public void Deve_Ser_Invalido_Quando_Nao_Houver_Assuntos()
    {
        // Arrange
        var comand = new LivroAtualizarComand(
            id: 1,
            titulo: "Título válido",
            editora: "Editora válida",
            edicao: 1,
            anoPublicacao: "2023",
            autores: new[] { 1, 2 },
            assuntos: Array.Empty<int>()
        );

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "Assuntos" && n.Message == "Assunto(s) não informado(s)!");
    }
}
