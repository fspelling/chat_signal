using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Domain.Interfaces.Repository.Base;

namespace POC.ChatSignal.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<Usuario?> BuscarPorEmail(string email);
        Task<Usuario?> BuscarPorEmailSenha(string email, string senha);
    }
}
