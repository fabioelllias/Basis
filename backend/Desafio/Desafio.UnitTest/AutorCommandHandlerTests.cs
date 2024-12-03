using AutoMapper;
using Desafio.Application;
using Desafio.Core.Entidades;
using Desafio.Infrastructure;
using FluentAssertions;
using Moq;

namespace Desafio.UnitTest;

[TestFixture]
public class AutorCommandHandlerTests
{
    private Mock<INotificationContext> _notificationMock;
    private Mock<IRepositoryBase<Autor>> _repositoryMock;
    private Mock<IMapper> _mapperMock;
    private Mock<ICommandResultFactory> _factoryMock;
    private AutorCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _notificationMock = new Mock<INotificationContext>();
        _repositoryMock = new Mock<IRepositoryBase<Autor>>();
        _mapperMock = new Mock<IMapper>();
        _factoryMock = new Mock<ICommandResultFactory>();

        _handler = new AutorCommandHandler(
            _notificationMock.Object,
            _repositoryMock.Object,
            _mapperMock.Object,
            _factoryMock.Object);
    }

    [Test]
    public async Task Deve_Criar_Autor_Quando_Dados_Validos()
    {
        // Arrange
        var request = new AutorCriarComand("Nome Válido");
        var autor = new Autor { Id = 1, Nome = "Nome Válido" };
        var result = new AutorCriarResult { Id = 1, Nome = "Nome Válido" };

        _mapperMock.Setup(m => m.Map<Autor>(request)).Returns(autor);
        _repositoryMock.Setup(r => r.Save(autor));
        _mapperMock.Setup(m => m.Map<AutorCriarResult>(autor)).Returns(result);
        _factoryMock.Setup(f => f.Create(true, null, result))
                    .Returns(new CommandResult(true, null, result));

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Content.Should().BeEquivalentTo(result);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Autor>()), Times.Once);
    }

    [Test]
    public async Task Deve_Notificar_E_Falhar_Quando_Dados_Criar_Autor_Sao_Invalidos()
    {
        // Arrange
        var request = new AutorCriarComand(""); // Nome inválido
        request.IsValid.Should().BeFalse();

        _factoryMock.Setup(f => f.Create()).Returns(new CommandResult{ Success = false });

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        _notificationMock.Verify(n => n.AddNotification(request.Notifications), Times.Once);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Autor>()), Times.Never);
    }

    [Test]
    public async Task Deve_Atualizar_Autor_Quando_Registro_Existe()
    {
        // Arrange
        var request = new AutorAtualizarComand(1, "Nome Atualizado");
        var autor = new Autor { Id = 1, Nome = "Nome Atualizado" };
        var result = new AutorAtualizarResult { Id = 1, Nome = "Nome Atualizado" };

        _repositoryMock.Setup(r => r.GetAll()).Returns(new List<Autor> { new Autor { Id = 1 } }.AsQueryable());
        _mapperMock.Setup(m => m.Map<Autor>(request)).Returns(autor);
        _repositoryMock.Setup(r => r.Save(autor));
        _mapperMock.Setup(m => m.Map<AutorAtualizarResult>(autor)).Returns(result);
        _factoryMock.Setup(f => f.Create(true, null, result))
                    .Returns(new CommandResult(true, null, result));

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Content.Should().BeEquivalentTo(result);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Autor>()), Times.Once);
    }

    [Test]
    public async Task Deve_Notificar_E_Falhar_Quando_Autor_Nao_Encontrado_Ao_Atualizar()
    {
        // Arrange
        var request = new AutorAtualizarComand(1, "Nome Atualizado");

        _repositoryMock.Setup(r => r.GetAll()).Returns(Enumerable.Empty<Autor>().AsQueryable());
        _factoryMock.Setup(f => f.Create()).Returns(new CommandResult { Success = false });

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        _notificationMock.Verify(n => n.AddNotification("Autor", "Autor não encontrado!"), Times.Once);
    }
    
    [Test]
    public async Task Deve_Excluir_Autor_Quando_Registro_Existe()
    {
        // Arrange
        var request = new AutorExcluirComand(1);

        _repositoryMock.Setup(r => r.GetAll()).Returns(new List<Autor> { new Autor { Id = 1 } }.AsQueryable());
        _repositoryMock.Setup(r => r.Delete(1));
        _factoryMock.Setup(f => f.Create()).Returns(new CommandResult { Success = true });

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        _repositoryMock.Verify(r => r.Delete(1), Times.Once);
    }

    [Test]
    public async Task Deve_Notificar_E_Falhar_Quando_Autor_Nao_Encontrado_Ao_Excluir()
    {
        // Arrange
        var request = new AutorExcluirComand(1);

        _repositoryMock.Setup(r => r.GetAll()).Returns(Enumerable.Empty<Autor>().AsQueryable());
        _factoryMock.Setup(f => f.Create()).Returns(new CommandResult{ Success = false });

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        _notificationMock.Verify(n => n.AddNotification("Autor", "Autor não encontrado!"), Times.Once);
    }





}


