using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Domain.ViewModel.Models;

namespace POC.ChatSignal.Domain.ViewModel.Hub.Chat.Response
{
    public class CriarGrupoUsuarioResponse
    {
        public required GrupoConections GrupoConections { get; set; }
        public List<Mensagem>? Mensagens { get; set; }
    }
}
