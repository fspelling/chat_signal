using Microsoft.EntityFrameworkCore;
using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Domain.Interfaces.Repository;
using POC.ChatSignal.Sql.Repository.Base;

namespace POC.ChatSignal.Sql.Repository
{
    public class MensagemRepository(ChatDbContext dbContext) : RepositoryBase<Mensagem>(dbContext), IMensagemRepository
    {
        public async Task<List<Mensagem>?> ListarPorNomeGrupo(string nomeGrupo)
            => await dbContext.Mensagens.Where(m => m.NomeGrupo == nomeGrupo).ToListAsync();
    }
}
