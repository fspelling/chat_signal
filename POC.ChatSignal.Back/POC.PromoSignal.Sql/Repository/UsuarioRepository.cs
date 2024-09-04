using Microsoft.EntityFrameworkCore;
using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Domain.Interfaces.Repository;
using POC.ChatSignal.Sql.Repository.Base;

namespace POC.ChatSignal.Sql.Repository
{
    public class UsuarioRepository(ChatDbContext dbContext) : RepositoryBase<Usuario>(dbContext), IUsuarioRepository
    {
        public async Task<Usuario?> BuscarPorEmail(string email)
            => await dbContext.Usuarios.Where(u => u.Email == email).FirstOrDefaultAsync();

        public async Task<Usuario?> BuscarPorEmailSenha(string email, string senha)
            => await dbContext.Usuarios.Where(u => u.Email == email && u.Senha == senha).FirstOrDefaultAsync();
    }
}
