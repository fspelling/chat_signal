namespace POC.ChatSignal.Domain.ViewModel.Hub.Chat.Request
{
    public class CriarGrupoUsuarioRequest
    {
        public required string EmailLogado { get; set; }
        public required string EmailConversa { get; set; }
    }
}
