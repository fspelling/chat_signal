using POC.ChatSignal.Domain.ViewModel.Api.Usuario.Request;
using POC.ChatSignal.Domain.ViewModel.Api.Usuario.Response;

namespace POC.ChatSignal.Domain.Interfaces.Service
{
    public interface IUsuarioService
    {
        Task Criar(CadastrarUsuarioRequest request);
        Task<LoginResponse> Login(LoginRequest request);
        Task<ObterUsuariosResponse> ObterUsuarios();
    }
}
