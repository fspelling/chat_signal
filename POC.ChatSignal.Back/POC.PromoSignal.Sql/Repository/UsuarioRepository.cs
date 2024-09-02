using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Domain.Interfaces.Repository;
using POC.ChatSignal.Sql.Repository.Base;

namespace POC.ChatSignal.Sql.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
    }
}
