using Desafio.Core.Entidades;
using Desafio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Infrastructure
{
    public class LivroContexto : DbContext, IUnitOfWork
    {
        public LivroContexto(DbContextOptions<LivroContexto> options) : base(options) { }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<LivroAutor> LivroAutores { get; set; }
        public DbSet<LivroAssunto> LivroAssuntos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=postgres;Database=livrosdb;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>(entity =>
            {
                entity.ToTable("Livro");
                entity.Property(l => l.Id).HasColumnName("Codl");
                entity.Property(l => l.Titulo).HasMaxLength(40).IsRequired();
                entity.Property(l => l.Editora).HasMaxLength(40).IsRequired();
                entity.Property(l => l.Edicao).IsRequired();
                entity.Property(l => l.AnoPublicacao).HasMaxLength(4).IsFixedLength(true).IsRequired();
            });

            modelBuilder.Entity<Autor>(entity =>
            {
                entity.ToTable("Autor");
                entity.Property(a => a.Id).HasColumnName("CodAu");
                entity.Property(l => l.Nome).HasMaxLength(40).IsRequired();
            });

            modelBuilder.Entity<Assunto>(entity =>
            {
                entity.ToTable("Assunto");
                entity.Property(a => a.Id).HasColumnName("codAs");
                entity.Property(a => a.Descricao).HasMaxLength(20).IsRequired();
            });

            modelBuilder.Entity<LivroAutor>()
                .ToTable("Livro_Autor")
                .HasKey(la => new { la.LivroId, la.AutorId });

            modelBuilder.Entity<LivroAutor>()
                .Property(la => la.LivroId)
                .HasColumnName("Livro_Codl");

            modelBuilder.Entity<LivroAutor>()
                .Property(la => la.AutorId)
                .HasColumnName("Autor_CodAu");

            modelBuilder.Entity<LivroAutor>()
                .HasOne(la => la.Livro)
                .WithMany(l => l.LivroAutores)
                .HasForeignKey(la => la.LivroId);

            modelBuilder.Entity<LivroAutor>()
                .HasOne(la => la.Autor)
                .WithMany(a => a.LivroAutores)
                .HasForeignKey(la => la.AutorId);

            modelBuilder.Entity<LivroAssunto>()
                .ToTable("Livro_Assunto")
                .HasKey(la => new { la.LivroId, la.AssuntoId });

            modelBuilder.Entity<LivroAssunto>()
                .Property(la => la.LivroId)
                .HasColumnName("Livro_Codl");

            modelBuilder.Entity<LivroAssunto>()
                .Property(la => la.AssuntoId)
                .HasColumnName("Assunto_codAs");

            modelBuilder.Entity<LivroAssunto>()
                .HasOne(la => la.Livro)
                .WithMany(l => l.LivroAssuntos)
                .HasForeignKey(la => la.LivroId);

            modelBuilder.Entity<LivroAssunto>()
                .HasOne(la => la.Assunto)
                .WithMany(a => a.LivroAssuntos)
                .HasForeignKey(la => la.AssuntoId);
        }

        public void SeedData()
        {
            if (!Autores.Any())
            {
                var autores = new List<Autor>
                {
                    new Autor { Nome = "José de Alencar" },
                    new Autor { Nome = "Cecília Meireles" },
                    new Autor { Nome = "Machado de Assis" }
                };
                Autores.AddRange(autores);
                SaveChanges();
            }

            if (!Assuntos.Any())
            {
                var assuntos = new List<Assunto>
                {
                    new Assunto { Descricao = "Ficção Científica" },
                    new Assunto { Descricao = "História" },
                    new Assunto { Descricao = "Tecnologia" }
                };
                Assuntos.AddRange(assuntos);
                SaveChanges();
            }

            if (!Livros.Any())
            {
                var livros = new List<Livro>
                {
                    new Livro { Titulo = "Abc do amanhã", Editora = "Editora Elementar", Edicao = 1, AnoPublicacao = "2020" },
                    new Livro { Titulo = "Caranguejo voador", Editora = "Editora Globo", Edicao = 2, AnoPublicacao = "2019" },
                    new Livro { Titulo = "Ordem ou Progresso", Editora = "Editora Studio", Edicao = 3, AnoPublicacao = "2021" }
                };
                Livros.AddRange(livros);
                SaveChanges();
            }

            if (!LivroAutores.Any())
            {
                var livros = Livros.ToList();
                var autores = Autores.ToList();
                var livroAutores = new List<LivroAutor>
                {
                    new LivroAutor { Livro = livros[0], Autor = autores[0] },
                    new LivroAutor { Livro = livros[1], Autor = autores[1] },
                    new LivroAutor { Livro = livros[2], Autor = autores[2] },
                    new LivroAutor { Livro = livros[0], Autor = autores[1] }
                };
                LivroAutores.AddRange(livroAutores);
                SaveChanges();
            }

            if (!LivroAssuntos.Any())
            {
                var livros = Livros.ToList();
                var assuntos = Assuntos.ToList();
                var livroAssuntos = new List<LivroAssunto>
                {
                    new LivroAssunto { Livro = livros[0], Assunto = assuntos[0] },
                    new LivroAssunto { Livro = livros[1], Assunto = assuntos[1] },
                    new LivroAssunto { Livro = livros[2], Assunto = assuntos[2] },
                    new LivroAssunto { Livro = livros[0], Assunto = assuntos[1] }
                };
                LivroAssuntos.AddRange(livroAssuntos);
                SaveChanges();
            }
        }
    }
}