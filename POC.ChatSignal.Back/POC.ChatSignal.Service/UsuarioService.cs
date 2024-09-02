using POC.ChatSignal.Domain.Interfaces.Repository;
using POC.ChatSignal.Domain.Interfaces.Service;

namespace POC.ChatSignal.Service
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {
        public readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    }
}
