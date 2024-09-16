using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Domain.Interfaces.Repository.Base;

namespace POC.ChatSignal.Domain.Interfaces.Repository
{
    public interface IGrupoRepository : IRepositoryBase<Grupo>
    {
        Task<Grupo?> BuscarPorNome(string nome);
        Task<List<Grupo>> ObterPorUsuario(string emailUsuario);
    }
}
