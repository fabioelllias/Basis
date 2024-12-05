using Desafio.Core;
using Desafio.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Desafio.Infrastructure
{
    public class LivroRepository : RepositoryBase<Livro>, ILivroRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public LivroRepository(IUnitOfWork context) : base(context)
        {
            _unitOfWork = context;
        }

        public void Atualizar(Livro updated)
        {
            var entity = base.GetById(updated.Id, "LivroAutores", "LivroAssuntos");

            var autoresParaRemover = entity.LivroAutores.Except(updated.LivroAutores).ToList();
            var autoresParaIncluir = updated.LivroAutores.Except(entity.LivroAutores).ToList();

            var assuntosParaRemover = entity.LivroAssuntos.Except(updated.LivroAssuntos).ToList();
            var assuntosParaIncluir = updated.LivroAssuntos.Except(entity.LivroAssuntos).ToList();

            _unitOfWork.Entry(entity).State = EntityState.Detached;
            base.Save(updated);
           
            foreach (var item in autoresParaRemover)
                _unitOfWork.Database.ExecuteSqlRaw($"DELETE FROM \"Livro_Autor\" WHERE \"Livro_Codl\"={item.LivroId} AND \"Autor_CodAu\"={item.AutorId}");

            foreach (var item in autoresParaIncluir)
                _unitOfWork.Database.ExecuteSqlRaw($"INSERT INTO \"Livro_Autor\" (\"Livro_Codl\", \"Autor_CodAu\") VALUES({item.LivroId}, {item.AutorId});");

            foreach (var item in assuntosParaRemover)
                _unitOfWork.Database.ExecuteSqlRaw($"DELETE FROM \"Livro_Assunto\" WHERE \"Livro_Codl\"={item.LivroId} AND \"Assunto_codAs\"={item.AssuntoId};");

            foreach (var item in assuntosParaIncluir)
                _unitOfWork.Database.ExecuteSqlRaw($"INSERT INTO \"Livro_Assunto\" (\"Livro_Codl\", \"Assunto_codAs\") VALUES({item.LivroId}, {item.AssuntoId});");
        }
    }
}
