using Desafio.Core;
using Desafio.Core.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Infrastructure
{
    public class AutorRepository : RepositoryBase<Autor>, IAutorRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public AutorRepository(IUnitOfWork context) : base(context)
        {
            _unitOfWork = context;
        }

        public List<AutorReportDto> Listar()
        {
            return _unitOfWork.Database.SqlQueryRaw<AutorReportDto>("select * from vw_autor_livro_assunto").ToList();
        }
    }
}
