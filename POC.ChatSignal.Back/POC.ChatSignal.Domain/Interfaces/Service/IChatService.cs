using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Domain.ViewModel.Hub.Chat.Request;
using POC.ChatSignal.Domain.ViewModel.Hub.Chat.Response;

namespace POC.ChatSignal.Domain.Interfaces.Service
{
    public interface IChatService
    {
        Task<AtualizarConnectionResponse> AtualizarConnection(AtualizarConnectionRequest request);
        Task<RemoverConnectionResponse> RemoverConnection(RemoverConnectionRequest request);
        Task<CriarGrupoUsuarioResponse> CriarGrupoUsuario(CriarGrupoUsuarioRequest request);
        Task<EnviarMensagemResponse> EnviarMensagem(EnviarMensagemRequest request);
    }
}
