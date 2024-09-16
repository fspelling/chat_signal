namespace POC.ChatSignal.Domain.ViewModel.Api.Usuario.Request
{
    public class CadastrarUsuarioRequest
    {
        public required string UsuarioNome { get; set; }
        public required string UsuarioEmail { get; set; }
        public required string UsuarioSenha { get; set; }
    }
}
