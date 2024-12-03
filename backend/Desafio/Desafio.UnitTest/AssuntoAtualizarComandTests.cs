namespace Desafio.UnitTest;

using Desafio.Application;
using FluentAssertions;
using NUnit.Framework;

[TestFixture]
public class AssuntoAtualizarComandTests
{
    [Test]
    public void Deve_Ser_Valido_Quando_Id_E_Descricao_Sao_Validos()
    {
        // Arrange
        var id = 1;
        var descricao = "Descrição válida";

        // Act
        var comand = new AssuntoAtualizarComand(id, descricao);

        // Assert
        comand.IsValid.Should().BeTrue();
        comand.Notifications.Should().BeEmpty();
    }

    [TestCase("")]
    public void Deve_Ser_Invalido_Quando_Descricao_Nula_Ou_Vazia(string descricao)
    {
        // Arrange
        var id = 1;

        // Act
        var comand = new AssuntoAtualizarComand(id, descricao);

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "Descricao" && n.Message == "Descricao não informada!");
    }

    [TestCase("A")] // Descrição muito curta
    [TestCase("Descrição muito longa que excede o limite de 20 caracteres")] // Descrição muito longa
    public void Deve_Ser_Invalido_Quando_Tamanho_Da_Descricao_For_Invalido(string descricao)
    {
        // Arrange
        var id = 1;

        // Act
        var comand = new AssuntoAtualizarComand(id, descricao);

        // Assert
        comand.IsValid.Should().BeFalse();

        if (descricao.Length < 2)
        {
            comand.Notifications.Should().ContainSingle(n =>
                n.Key == "Descricao" && n.Message == "Tamanho mínimo 2 caracteres!");
        }
        else if (descricao.Length > 20)
        {
            comand.Notifications.Should().ContainSingle(n =>
                n.Key == "Descricao" && n.Message == "Tamanho máximo 20 caracteres!");
        }
    }

    [TestCase(0)]  // ID igual a 0
    [TestCase(-1)] // ID negativo
    public void Deve_Ser_Invalido_Quando_Id_For_Invalido(int id)
    {
        // Arrange
        var descricao = "Descrição válida";

        // Act
        var comand = new AssuntoAtualizarComand(id, descricao);

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "Id" && n.Message == "Id inválido!");
    }

    [TestCase("AB")] // Descrição no limite mínimo
    [TestCase("Descrição limite")] // Descrição no limite máximo
    public void Deve_Ser_Valido_Quando_Descricao_Esta_No_Limite(string descricao)
    {
        // Arrange
        var id = 1;

        // Act
        var comand = new AssuntoAtualizarComand(id, descricao);

        // Assert
        comand.IsValid.Should().BeTrue();
        comand.Notifications.Should().BeEmpty();
    }
}


