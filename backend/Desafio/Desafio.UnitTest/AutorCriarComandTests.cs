using Desafio.Application;
using FluentAssertions;

namespace Desafio.UnitTest;


[TestFixture]
public class AutorCriarComandTests
{
    [Test]
    public void Deve_Ser_Valido_Quando_Nome_Valido()
    {
        // Arrange
        var nome = "Nome Válido";

        // Act
        var comand = new AutorCriarComand(nome);

        // Assert
        comand.IsValid.Should().BeTrue();
        comand.Notifications.Should().BeEmpty();
    }

    [TestCase("")]
    public void Deve_Ser_Invalido_Quando_Nome_Nulo_Ou_Vazio(string nome)
    {
        // Act
        var comand = new AutorCriarComand(nome);

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "Nome" && n.Message == "Nome não informado!");
    }

    [TestCase("A")] // Nome muito curto
    [TestCase("Este nome tem mais de quarenta caracteres e será inválido")] // Nome muito longo
    public void Deve_Ser_Invalido_Quando_Tamanho_Do_Nome_For_Invalido(string nome)
    {
        // Act
        var comand = new AutorCriarComand(nome);

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

    [TestCase("AB")] // Nome no limite mínimo
    [TestCase("Este tem exatamente quarenta caracteres!")] // Nome no limite máximo
    public void Deve_Ser_Valido_Quando_Tamanho_Do_Nome_For_No_Limite(string nome)
    {
        // Act
        var comand = new AutorCriarComand(nome);

        // Assert
        comand.IsValid.Should().BeTrue();
        comand.Notifications.Should().BeEmpty();
    }
}
