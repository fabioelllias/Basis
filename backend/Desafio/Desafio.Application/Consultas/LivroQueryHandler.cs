using AutoMapper;
using Desafio.Core.Entidades;
using Desafio.Infrastructure;
using Flunt.Notifications;
using MediatR;

namespace Desafio.Application
{
    public class LivroQueryHandler : Notifiable<Notification>,
                                     IRequestHandler<LivroListarTodosQuery, CommandResult>,
                                     IRequestHandler<LivroObterPorIdQuery, CommandResult>,
                                     IRequestHandler<LivroFormaCompraQuery, CommandResult>,
                                     IRequestHandler<LivroPrecoQuery, CommandResult>

    {
        private readonly IRepositoryBase<Livro> _repository;
        private readonly IRepositoryBase<FormaCompra> _repositoryFormaCompra;
        private readonly INotificationContext _notification;
        private readonly IMapper _mapper;
        private readonly ICommandResultFactory _factory;

        public LivroQueryHandler(IRepositoryBase<Livro> repository, INotificationContext notification, IMapper mapper, ICommandResultFactory factory, IRepositoryBase<FormaCompra> repositoryFormaCompra)
        {
            _repository = repository;
            _notification = notification;
            _mapper = mapper;
            _factory = factory;
            _repositoryFormaCompra = repositoryFormaCompra;
        }

        public async Task<CommandResult> Handle(LivroListarTodosQuery request, CancellationToken cancellationToken)
        {
            var entitys = _repository.GetAll("LivroAutores.Autor", "LivroAssuntos.Assunto").ToList();

            var response = _mapper.Map<List<LivroResult>>(entitys);

            return _factory.Create(true, null, response);
        }

        public async Task<CommandResult> Handle(LivroObterPorIdQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notification.AddNotification(request.Notifications);
                return _factory.Create();
            }

            var entity = _repository.GetById(request.Id, "LivroAutores.Autor", "LivroAssuntos.Assunto");
            if (entity == null)
            {
                _notification.AddNotification("Livro", "Livro não encontrado!");
                return _factory.Create();
            }

            var response = _mapper.Map<LivroResult>(entity);

            return _factory.Create(true, null, response);
        }

        public async Task<CommandResult> Handle(LivroFormaCompraQuery request, CancellationToken cancellationToken)
        {
            var entitys = _repositoryFormaCompra.GetAll().ToList();

            var response = _mapper.Map<List<LivroFormaCompraResult>>(entitys);

            return _factory.Create(true, null, response);
        }

        public async Task<CommandResult> Handle(LivroPrecoQuery request, CancellationToken cancellationToken)
        {

            var entity = _repository.GetById(request.Id, "LivroPrecos.FormaCompra");
            if (entity == null)
            {
                _notification.AddNotification("Livro", "Livro não encontrado!");
                return _factory.Create();
            }

            var response = _mapper.Map<LivroPrecoResult>(entity);

            return _factory.Create(true, null, response);
        }
    }
}