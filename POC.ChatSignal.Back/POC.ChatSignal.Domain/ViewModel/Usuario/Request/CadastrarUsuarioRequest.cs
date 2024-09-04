namespace POC.ChatSignal.Domain.ViewModel.Usuario.Request
{
    public class CadastrarUsuarioRequest
    {
        public required string UsuarioNome { get; set; }
        public required string UsuarioEmail { get; set; }
        public required string UsuarioSenha { get; set; }
    }
}
