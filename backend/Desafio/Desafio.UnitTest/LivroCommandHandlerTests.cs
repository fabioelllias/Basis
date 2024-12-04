using AutoMapper;
using Desafio.Application;
using Desafio.Core.Entidades;
using Desafio.Infrastructure;
using FluentAssertions;
using Moq;

namespace Desafio.UnitTest;


[TestFixture]
public class LivroCommandHandlerTests
{
    private Mock<INotificationContext> _notificationMock;
    private Mock<IRepositoryBase<Livro>> _repositoryMock;
    private Mock<IMapper> _mapperMock;
    private Mock<ICommandResultFactory> _factoryMock;
    private LivroCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _notificationMock = new Mock<INotificationContext>();
        _repositoryMock = new Mock<IRepositoryBase<Livro>>();
        _mapperMock = new Mock<IMapper>();
        _factoryMock = new Mock<ICommandResultFactory>();

        _handler = new LivroCommandHandler(
            _notificationMock.Object,
            _repositoryMock.Object,
            _mapperMock.Object,
            _factoryMock.Object
        );
    }

    [Test]
    public async Task Deve_Criar_Livro_Quando_Dados_Sao_Validos()
    {
        // Arrange
        var request = new LivroCriarComand("Título", "Editora", 1, "2023", new[] { 1 }, new[] { 2 });
        var livro = new Livro { Id = 1, Titulo = "Título" };
        var livroResult = new LivroCriarResult { Id = 1, Titulo = "Título" };

        _mapperMock.Setup(m => m.Map<Livro>(request)).Returns(livro);
        _repositoryMock.Setup(r => r.Save(livro));
        _repositoryMock.Setup(r => r.GetById(livro.Id, "LivroAutores.Autor", "LivroAssuntos.Assunto")).Returns(livro);
        _mapperMock.Setup(m => m.Map<LivroCriarResult>(livro)).Returns(livroResult);
        _factoryMock.Setup(f => f.Create(true, null, livroResult)).Returns(new CommandResult(true, null, livroResult));

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Content.Should().BeEquivalentTo(livroResult);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Livro>()), Times.Once);
    }

    [Test]
    public async Task Deve_Notificar_Quando_Criar_Livro_Com_Dados_Invalidos()
    {
        // Arrange
        var request = new LivroCriarComand("", "", 0, "2023", new int[0], new int[0]);
        request.IsValid.Should().BeFalse();

        _factoryMock.Setup(f => f.Create()).Returns(new CommandResult { Success = false });

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        _notificationMock.Verify(n => n.AddNotification(request.Notifications), Times.Once);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Livro>()), Times.Never);
    }

    [Test]
    public async Task Deve_Atualizar_Livro_Quando_Registro_Existe()
    {
        // Arrange
        var request = new LivroAtualizarComand(1, "Título Atualizado", "Editora Atualizada", 1, "2023", new[] { 1 }, new[] { 2 });
        var livro = new Livro { Id = 1, Titulo = "Título Atualizado" };
        var livroResult = new LivroAtualizarResult { Id = 1, Titulo = "Título Atualizado" };

        _repositoryMock.Setup(r => r.GetAll()).Returns(new[] { livro }.AsQueryable());
        _mapperMock.Setup(m => m.Map<Livro>(request)).Returns(livro);
        _repositoryMock.Setup(r => r.Save(livro));
        _repositoryMock.Setup(r => r.GetById(livro.Id, "LivroAutores.Autor", "LivroAssuntos.Assunto")).Returns(livro);
        _mapperMock.Setup(m => m.Map<LivroAtualizarResult>(livro)).Returns(livroResult);
        _factoryMock.Setup(f => f.Create(true, null, livroResult)).Returns(new CommandResult(true, null, livroResult));

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        response.Content.Should().BeEquivalentTo(livroResult);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Livro>()), Times.Once);
    }

    [Test]
    public async Task Deve_Notificar_Quando_Livro_Nao_Encontrado_Ao_Atualizar()
    {
        // Arrange
        var request = new LivroAtualizarComand(1, "Título Atualizado", "Editora Atualizada", 1, "2023", new[] { 1 }, new[] { 2 });

        _repositoryMock.Setup(r => r.GetAll()).Returns(Enumerable.Empty<Livro>().AsQueryable());
        _factoryMock.Setup(f => f.Create()).Returns(new CommandResult{ Success = false });

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        _notificationMock.Verify(n => n.AddNotification("Livro", "Livro não encontrado!"), Times.Once);
    }

    [Test]
    public async Task Deve_Excluir_Livro_Quando_Registro_Existe()
    {
        // Arrange
        var request = new LivroExcluirComand(1);

        _repositoryMock.Setup(r => r.GetAll()).Returns(new[] { new Livro { Id = 1 } }.AsQueryable());
        _repositoryMock.Setup(r => r.Delete(request.Id));
        _factoryMock.Setup(f => f.Create()).Returns(new CommandResult { Success = true });

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        _repositoryMock.Verify(r => r.Delete(1), Times.Once);
    }

    [Test]
    public async Task Deve_Notificar_Quando_Livro_Nao_Encontrado_Ao_Excluir()
    {
        // Arrange
        var request = new LivroExcluirComand(1);

        _repositoryMock.Setup(r => r.GetAll()).Returns(Enumerable.Empty<Livro>().AsQueryable());
        _factoryMock.Setup(f => f.Create()).Returns(new CommandResult{ Success = false });

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        _notificationMock.Verify(n => n.AddNotification("Livro", "Livro não encontrado!"), Times.Once);
    }
}



