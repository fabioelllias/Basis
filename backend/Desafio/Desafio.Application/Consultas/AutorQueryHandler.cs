using MediatR;
using Flunt.Notifications;
using Desafio.Infrastructure;
using Desafio.Core.Entidades;
using AutoMapper;

namespace Desafio.Application
{
    public class AutorQueryHandler : Notifiable<Notification>, 
                                     IRequestHandler<AutorListarTodosQuery, CommandResult>
    {
        private readonly IRepositoryBase<Autor> _repository;
        private readonly INotificationContext _notification;
        private readonly IMapper _mapper;
        private readonly ICommandResultFactory _factory;

        public AutorQueryHandler(IRepositoryBase<Autor> repository, INotificationContext notification, IMapper mapper, ICommandResultFactory factory)
        {
            _repository = repository;
            _notification = notification;
            _mapper = mapper;
            _factory = factory;
        }

        public async Task<CommandResult> Handle(AutorListarTodosQuery request, CancellationToken cancellationToken)
        {
            var entitys = _repository.GetAll().ToList();

            var response = _mapper.Map<List<AutorResult>>(entitys);
            
            return _factory.Create(true, null, response);
        }
    }
}