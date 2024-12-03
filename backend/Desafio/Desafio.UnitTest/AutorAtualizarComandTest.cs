using NUnit.Framework;
using FluentAssertions;
using Desafio.Application;

namespace Desafio.UnitTest;

[TestFixture]
public class AutorAtualizarComandTests
{
    [Test]
    public void Deve_Ser_Valido_Quando_Nome_Valido()
    {
        // Arrange
        var id = 1;
        var nome = "Nome Válido";

        // Act
        var comand = new AutorAtualizarComand(id, nome);

        // Assert
        comand.IsValid.Should().BeTrue();
        comand.Notifications.Should().BeEmpty();
    }


    [TestCase("")]
    public void Deve_Ser_Invalido_Quando_Nome_Nulo_Ou_Vazio(string nome)
    {
        // Arrange
        var id = 1;

        // Act
        var comand = new AutorAtualizarComand(id, nome);

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "Nome" && n.Message == "Nome não informado!");
    }

    [TestCase("A")] // Nome muito curto
    [TestCase("Este nome tem mais de quarenta caracteres e será inválido")] // Nome muito longo
    public void Deve_Ser_Invalido_Quando_Tamanho_Do_Nome_For_Invalido(string nome)
    {
        // Arrange
        var id = 1;

        // Act
        var comand = new AutorAtualizarComand(id, nome);

        // Assert
        comand.IsValid.Should().BeFalse();

        if (nome.Length < 2)
        {
            comand.Notifications.Should().ContainSingle(n =>
                n.Key == "Nome" && n.Message == "Tamanho mínimo 2 caracteres!");
        }
        else if (nome.Length > 40)
        {
            comand.Notifications.Should().ContainSingle(n =>
                n.Key == "Nome" && n.Message == "Tamanho máximo 40 caracteres!");
        }
    }

    [TestCase(0)]  // ID igual a 0
    [TestCase(-1)] // ID negativo
    public void Deve_Ser_Valido_Quando_Id_Igual_Zero_Ou_Negativo(int id)
    {
        // Arrange
        var nome = "Nome Válido";

        // Act
        var comand = new AutorAtualizarComand(id, nome);

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "Id" && n.Message == "Id inválido!");
    }

    [Test]
    public void Deve_Ser_Invalido_Com_Multiplos_Valores_Invalidos()
    {
        // Arrange
        var id = -1; // ID inválido
        var nome = "A"; // Nome muito curto

        // Act
        var comand = new AutorAtualizarComand(id, nome);

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().HaveCount(2);

        comand.Notifications.Should().Contain(n =>
            n.Key == "Id" && n.Message == "Id inválido!");

        comand.Notifications.Should().Contain(n =>
            n.Key == "Nome" && n.Message == "Tamanho mínimo 2 caracteres!");
    }
}


