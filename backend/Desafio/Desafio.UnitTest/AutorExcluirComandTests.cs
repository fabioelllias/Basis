﻿using Desafio.Application;
using FluentAssertions;

namespace Desafio.UnitTest;

[TestFixture]
public class AutorExcluirComandTests
{
    [Test]
    public void Deve_Ser_Valido_Quando_Id_Valido()
    {
        // Arrange
        var id = 1;

        // Act
        var comand = AutorExcluirComand.Create(id);

        // Assert
        comand.IsValid.Should().BeTrue();
        comand.Notifications.Should().BeEmpty();
    }

    [TestCase(0)]  // ID igual a 0
    [TestCase(-1)] // ID negativo
    public void Deve_Ser_Invalido_Quando_Id_For_Invalido(int id)
    {
        // Act
        var comand = AutorExcluirComand.Create(id);

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "Id" && n.Message == "Id inválido!");
    }

    [Test]
    public void Deve_Criar_Comando_Quando_Id_Valido()
    {
        // Arrange
        var id = 5;

        // Act
        var comand = AutorExcluirComand.Create(id);

        // Assert
        comand.Id.Should().Be(id);
        comand.IsValid.Should().BeTrue();
        comand.Notifications.Should().BeEmpty();
    }

    [Test]
    public void Deve_Falhar_Ao_Criar_Comando_Quando_Id_For_Invalido()
    {
        // Arrange
        var id = -10;

        // Act
        var comand = AutorExcluirComand.Create(id);

        // Assert
        comand.IsValid.Should().BeFalse();
        comand.Notifications.Should().ContainSingle(n =>
            n.Key == "Id" && n.Message == "Id inválido!");
    }
}

