namespace POC.ChatSignal.Domain.ViewModel.Api.Usuario.Request
{
    public class LoginRequest
    {
        public required string UsuarioEmail { get; set; }
        public required string UsuarioSenha { get; set; }
    }
}
