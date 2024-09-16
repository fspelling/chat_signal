using Microsoft.AspNetCore.SignalR;
using POC.ChatSignal.Domain.Interfaces.Service;
using POC.ChatSignal.Domain.ViewModel.Hub.Chat.Request;

namespace POC.ChatSignal.API.Hubs
{
    public class ChatHub(IChatService chatService) : Hub
    {
        public readonly IChatService _chatService = chatService;

        public async Task AtualizarConnection(AtualizarConnectionRequest request)
        {
            var response = await _chatService.AtualizarConnection(request);
            await Clients.Others.SendAsync("atualizarUsuarioConversacao", response.Usuario);

            response.GruposConections.ForEach(grupoConection =>
            {
                grupoConection.ConectionIds.ForEach(async connectionId => 
                    await Groups.AddToGroupAsync(connectionId, grupoConection.GrupoNome));
            });
        }

        public async Task RemoverConnection(RemoverConnectionRequest request)
        {
            var response = await _chatService.RemoverConnection(request);
            await Clients.Others.SendAsync("atualizarUsuarioConversacao", response.Usuario);

            response.GruposConections.ForEach(grupoConection =>
            {
                grupoConection.ConectionIds.ForEach(async connectionId =>
                    await Groups.RemoveFromGroupAsync(connectionId, grupoConection.GrupoNome));
            });
        }

        public async Task CriarGrupoUsuario(CriarGrupoUsuarioRequest request)
        {
            var response = await _chatService.CriarGrupoUsuario(request);

            response.GrupoConections.ConectionIds.ForEach(async connectionId => 
                await Groups.AddToGroupAsync(connectionId, response.GrupoConections.GrupoNome));

            await Clients.Caller.SendAsync("abrirGrupo", response.GrupoConections.GrupoNome, response.Mensagens);
        }

        public async Task EnviarMensagem(EnviarMensagemRequest request)
        {
            var response = await _chatService.EnviarMensagem(request);
            await Clients.Group(request.NomeGrupo).SendAsync("receberMensagem", response.Mensagem);
        }
    }
}
