using AutoMapper;
using Desafio.Core;
using Desafio.Core.Entidades;
using Desafio.Infrastructure;
using Flunt.Notifications;
using MediatR;

namespace Desafio.Application
{
    public class LivroCommandHandler : Notifiable<Notification>,
                                        IRequest<CommandResult>,
                                        IRequestHandler<LivroCriarComand, CommandResult>,
                                        IRequestHandler<LivroAtualizarComand, CommandResult>,      
                                        IRequestHandler<LivroExcluirComand, CommandResult>        

    {
        private readonly INotificationContext _notification;
        private readonly IRepositoryBase<Livro> _repository;
        private readonly ILivroRepository _repositoryLivro;
        private readonly IMapper _mapper;
        private readonly ICommandResultFactory _factory;

        public LivroCommandHandler(
            INotificationContext notificationContext, 
            IRepositoryBase<Livro> repository, 
            IMapper mapper, 
            ICommandResultFactory factory,
            ILivroRepository livroRepository)
        {
            this._notification = notificationContext;
            this._repository = repository;
            this._mapper = mapper;
            this._factory = factory;
            this._repositoryLivro = livroRepository;
        }

        public async Task<CommandResult> Handle(LivroCriarComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notification.AddNotification(request.Notifications);
                return _factory.Create();
            }

            var entity = _mapper.Map<Livro>(request);

            _repository.Save(entity);

            var newEntity = _repository.GetById(entity.Id, "LivroAutores.Autor", "LivroAssuntos.Assunto");

            var response = _mapper.Map<LivroCriarResult>(newEntity);

            return _factory.Create(true, null, response);
        }

        public async Task<CommandResult> Handle(LivroAtualizarComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notification.AddNotification(request.Notifications);
                return _factory.Create();
            }

            bool registroExiste = _repository.GetAll("LivroAutores.Autor", "LivroAssuntos.Assunto").Any(ent => ent.Id == request.Id);
            if (!registroExiste)
            {
                _notification.AddNotification("Livro", "Livro não encontrado!");
                return _factory.Create();
            }

            var entity = _mapper.Map<Livro>(request);

            _repositoryLivro.Atualizar(entity);

            var updatedEntity = _repository.GetById(entity.Id, "LivroAutores.Autor", "LivroAssuntos.Assunto");

            var response = _mapper.Map<LivroAtualizarResult>(updatedEntity);

            return _factory.Create(true, null, response);
        }

        public async Task<CommandResult> Handle(LivroExcluirComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notification.AddNotification(request.Notifications);
                return _factory.Create();
            }

            bool registroExiste = _repository.GetAll().Any(ent => ent.Id == request.Id);
            if (!registroExiste)
            {
                _notification.AddNotification("Livro", "Livro não encontrado!");
                return _factory.Create();
            }

            _repository.Delete(request.Id);

            return _factory.Create();
        }
    }
}