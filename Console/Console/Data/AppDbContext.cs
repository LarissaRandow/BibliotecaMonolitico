using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace Console.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Bibliotecario> Bibliotecarios { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Reserva> Reserva { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost; port=3306; database=biblioteca; user=root; password=panodeprato");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reserva>().Property(p => p.Cpf).HasMaxLength(11);
        }
    }
}
