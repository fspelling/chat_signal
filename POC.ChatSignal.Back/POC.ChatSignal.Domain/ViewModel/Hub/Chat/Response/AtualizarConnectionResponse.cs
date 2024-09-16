using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Domain.ViewModel.Models;

namespace POC.ChatSignal.Domain.ViewModel.Hub.Chat.Response
{
    public class AtualizarConnectionResponse
    {
        public required Usuario Usuario { get; set; }
        public required List<GrupoConections> GruposConections { get; set; }
    }
}
