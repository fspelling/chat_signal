using Microsoft.EntityFrameworkCore;
using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Domain.Interfaces.Repository;
using POC.ChatSignal.Sql.Repository.Base;

namespace POC.ChatSignal.Sql.Repository
{
    public class GrupoRepository(ChatDbContext dbContext) : RepositoryBase<Grupo>(dbContext), IGrupoRepository
    {
        public async Task<Grupo?> BuscarPorNome(string nome)
            => await dbContext.Grupos.Where(g => g.Nome == nome).FirstOrDefaultAsync();

        public async Task<List<Grupo>> ObterPorUsuario(string emailUsuario)
            => await dbContext.Grupos.Where(g => g.Usuarios.Contains(emailUsuario)).ToListAsync();
    }
}
