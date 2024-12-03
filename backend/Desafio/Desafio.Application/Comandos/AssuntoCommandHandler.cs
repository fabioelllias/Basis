using AutoMapper;
using Desafio.Core.Entidades;
using Desafio.Infrastructure;
using Flunt.Notifications;
using MediatR;

namespace Desafio.Application
{
    public class AssuntoCommandHandler : Notifiable<Notification>,
                                        IRequest<CommandResult>,
                                        IRequestHandler<AssuntoCriarComand, CommandResult>,
                                        IRequestHandler<AssuntoAtualizarComand, CommandResult>,      
                                        IRequestHandler<AssuntoExcluirComand, CommandResult>        

    {
        private readonly INotificationContext _notification;
        private readonly IRepositoryBase<Assunto> _repository;
        private readonly IMapper _mapper;
        private readonly ICommandResultFactory _factory;

        public AssuntoCommandHandler(INotificationContext notificationContext, IRepositoryBase<Assunto> repository, IMapper mapper, ICommandResultFactory factory)
        {
            this._notification = notificationContext;
            this._repository = repository;
            this._mapper = mapper;
            this._factory = factory;
        }

        public async Task<CommandResult> Handle(AssuntoCriarComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notification.AddNotification(request.Notifications);
                return _factory.Create();
            }

            var entity = _mapper.Map<Assunto>(request);

            _repository.Save(entity);

            var response = _mapper.Map<AssuntoCriarResult>(entity);

            return _factory.Create(true, null, response);
        }

        public async Task<CommandResult> Handle(AssuntoAtualizarComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notification.AddNotification(request.Notifications);
                return _factory.Create();
            }

            bool registroExiste = _repository.GetAll().Any(ent => ent.Id == request.Id);
            if (!registroExiste)
            {
                _notification.AddNotification("Assunto", "Assunto não encontrado!");
                return _factory.Create();
            }

            var entity = _mapper.Map<Assunto>(request);
            
            _repository.Save(entity);

            var response = _mapper.Map<AssuntoAtualizarResult>(entity);

            return _factory.Create(true, null, response);
        }

        public async Task<CommandResult> Handle(AssuntoExcluirComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notification.AddNotification(request.Notifications);
                return _factory.Create();
            }

            bool registroExiste = _repository.GetAll().Any(ent => ent.Id == request.Id);
            if (!registroExiste)
            {
                _notification.AddNotification("Assunto", "Assunto não encontrado!");
                return _factory.Create();
            }

            _repository.Delete(request.Id);

            return _factory.Create();
        }
    }
}