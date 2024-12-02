using Desafio.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Desafio.Infrastructure.Contexto
{
    public class LivroContexto : DbContext
    {
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
            modelBuilder.Entity<Livro>()
                .ToTable("Livro")
                .Property(l => l.Id)
                .HasColumnName("Cod");

            modelBuilder.Entity<Autor>()
                .ToTable("Autor")
                .Property(a => a.Id)
                .HasColumnName("CodAu");

            modelBuilder.Entity<Assunto>()
                .ToTable("Assunto")
                .Property(a => a.Id)
                .HasColumnName("CodAs");

            modelBuilder.Entity<LivroAutor>()
                .ToTable("Livro_Autor") 
                .HasKey(la => new { la.LivroId, la.AutorId });

            modelBuilder.Entity<LivroAutor>()
                .Property(la => la.LivroId)
                .HasColumnName("LivroCod"); 

            modelBuilder.Entity<LivroAutor>()
                .Property(la => la.AutorId)
                .HasColumnName("AutorCodAu");

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
                .HasColumnName("LivroCod"); 

            modelBuilder.Entity<LivroAssunto>()
                .Property(la => la.AssuntoId)
                .HasColumnName("AssuntoCodAs");

            modelBuilder.Entity<LivroAssunto>()
                .HasOne(la => la.Livro)
                .WithMany(l => l.LivroAssuntos)
                .HasForeignKey(la => la.LivroId);

            modelBuilder.Entity<LivroAssunto>()
                .HasOne(la => la.Assunto)
                .WithMany(a => a.LivroAssuntos)
                .HasForeignKey(la => la.AssuntoId);
        }
    }
}