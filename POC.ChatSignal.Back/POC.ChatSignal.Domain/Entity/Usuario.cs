using POC.ChatSignal.Domain.Entity.Base;

namespace POC.ChatSignal.Domain.Entity
{
    public class Usuario : EntityBase
    {
        public string? ConnectionIds { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public bool IsOnline { get; set; } = false;
    }
}
