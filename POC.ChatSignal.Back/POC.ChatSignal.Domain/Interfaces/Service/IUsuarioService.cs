using POC.ChatSignal.Domain.ViewModel.Usuario.Request;
using POC.ChatSignal.Domain.ViewModel.Usuario.Response;

namespace POC.ChatSignal.Domain.Interfaces.Service
{
    public interface IUsuarioService
    {
        Task Criar(CadastrarUsuarioRequest request);
        Task<LoginResponse> Login(LoginRequest request);
        Task AtualizarConnection(AtualizarConnectionRequest request);
        Task RemoverConnection(int usuarioId, string connectionId);
    }
}
