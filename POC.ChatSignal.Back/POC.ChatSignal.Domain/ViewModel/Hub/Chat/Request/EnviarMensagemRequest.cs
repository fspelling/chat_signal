using POC.ChatSignal.Domain.Entity;

namespace POC.ChatSignal.Domain.ViewModel.Hub.Chat.Request
{
    public class EnviarMensagemRequest
    {
        public required Usuario UsuarioLogado { get; set; }
        public required string Mensagem { get; set; }
        public required string NomeGrupo { get; set; }
    }
}
