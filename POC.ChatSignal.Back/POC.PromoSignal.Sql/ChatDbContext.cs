using Microsoft.EntityFrameworkCore;
using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Sql.EntityConfiguration;

namespace POC.ChatSignal.Sql
{
    public class ChatDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }

        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new GrupoConfiguration());
            modelBuilder.ApplyConfiguration(new MensagemConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
