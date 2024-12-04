using MediatR;
using Flunt.Notifications;
using Desafio.Infrastructure;
using Desafio.Core.Entidades;
using AutoMapper;

namespace Desafio.Application
{
    public class LivroQueryHandler : Notifiable<Notification>,
                                     IRequestHandler<LivroListarTodosQuery, CommandResult>,
                                     IRequestHandler<LivroObterPorIdQuery, CommandResult>
    {
        private readonly IRepositoryBase<Livro> _repository;
        private readonly INotificationContext _notification;
        private readonly IMapper _mapper;
        private readonly ICommandResultFactory _factory;

        public LivroQueryHandler(IRepositoryBase<Livro> repository, INotificationContext notification, IMapper mapper, ICommandResultFactory factory)
        {
            _repository = repository;
            _notification = notification;
            _mapper = mapper;
            _factory = factory;
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
    }
}