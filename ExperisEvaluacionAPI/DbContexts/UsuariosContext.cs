using ExperisEvaluacionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExperisEvaluacionAPI.DbContexts
{
    public class UsuariosContext : DbContext
    {
        public UsuariosContext(DbContextOptions<UsuariosContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Usuario>().Property(u => u.Genero).HasMaxLength(1);
        }
    }
}
