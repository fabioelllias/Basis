using MediatR;
using Flunt.Notifications;
using Desafio.Infrastructure;
using Desafio.Core.Entidades;
using AutoMapper;

namespace Desafio.Application
{
    public class AssuntoQueryHandler : Notifiable<Notification>,
                                     IRequestHandler<AssuntoListarTodosQuery, CommandResult>,
                                     IRequestHandler<AssuntoObterPorIdQuery, CommandResult>
    {
        private readonly IRepositoryBase<Assunto> _repository;
        private readonly INotificationContext _notification;
        private readonly IMapper _mapper;
        private readonly ICommandResultFactory _factory;

        public AssuntoQueryHandler(IRepositoryBase<Assunto> repository, INotificationContext notification, IMapper mapper, ICommandResultFactory factory)
        {
            _repository = repository;
            _notification = notification;
            _mapper = mapper;
            _factory = factory;
        }

        public async Task<CommandResult> Handle(AssuntoListarTodosQuery request, CancellationToken cancellationToken)
        {
            var entitys = _repository.GetAll().ToList();

            var response = _mapper.Map<List<AssuntoResult>>(entitys);

            return _factory.Create(true, null, response);
        }

        public async Task<CommandResult> Handle(AssuntoObterPorIdQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notification.AddNotification(request.Notifications);
                return _factory.Create();
            }

            var entity = _repository.GetById(request.Id);
            if (entity == null)
            {
                _notification.AddNotification("Assunto", "Assunto não encontrado!");
                return _factory.Create();
            }

            var response = _mapper.Map<AssuntoResult>(entity);

            return _factory.Create(true, null, response);
        }
    }
}