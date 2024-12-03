using AutoMapper;
using Desafio.Core.Entidades;
using Desafio.Infrastructure;
using Flunt.Notifications;
using MediatR;

namespace Desafio.Application
{
    public class AutorCommandHandler : Notifiable<Notification>,
                                        IRequest<CommandResult>,
                                        IRequestHandler<AutorCriarComand, CommandResult>,
                                        IRequestHandler<AutorAtualizarComand, CommandResult>,      
                                        IRequestHandler<AutorExcluirComand, CommandResult>        

    {
        private readonly INotificationContext _notification;
        private readonly IRepositoryBase<Autor> _repository;
        private readonly IMapper _mapper;
        private readonly ICommandResultFactory _factory;

        public AutorCommandHandler(INotificationContext notificationContext, IRepositoryBase<Autor> repository, IMapper mapper, ICommandResultFactory factory)
        {
            this._notification = notificationContext;
            this._repository = repository;
            this._mapper = mapper;
            this._factory = factory;
        }

        public async Task<CommandResult> Handle(AutorCriarComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notification.AddNotification(request.Notifications);
                return _factory.Create();
            }

            var entity = _mapper.Map<Autor>(request);

            _repository.Save(entity);

            var response = _mapper.Map<AutorCriarResult>(entity);

            return _factory.Create(true, null, response);
        }

        public async Task<CommandResult> Handle(AutorAtualizarComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notification.AddNotification(request.Notifications);
                return _factory.Create();
            }

            bool registroExiste = _repository.GetAll().Any(ent => ent.Id == request.Id);
            if (!registroExiste)
            {
                _notification.AddNotification("Autor", "Autor não encontrado!");
                return _factory.Create();
            }

            var entity = _mapper.Map<Autor>(request);
            
            _repository.Save(entity);

            var response = _mapper.Map<AutorAtualizarResult>(entity);

            return _factory.Create(true, null, response);
        }

        public async Task<CommandResult> Handle(AutorExcluirComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notification.AddNotification(request.Notifications);
                return _factory.Create();
            }

            bool registroExiste = _repository.GetAll().Any(ent => ent.Id == request.Id);
            if (!registroExiste)
            {
                _notification.AddNotification("Autor", "Autor não encontrado!");
                return _factory.Create();
            }

            _repository.Delete(request.Id);

            return _factory.Create();
        }
    }
}