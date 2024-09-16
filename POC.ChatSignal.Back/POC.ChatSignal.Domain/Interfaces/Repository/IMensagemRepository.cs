using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Domain.Interfaces.Repository.Base;

namespace POC.ChatSignal.Domain.Interfaces.Repository
{
    public interface IMensagemRepository : IRepositoryBase<Mensagem>
    {
        Task<List<Mensagem>?> ListarPorNomeGrupo(string nomeGrupo);
    }
}
