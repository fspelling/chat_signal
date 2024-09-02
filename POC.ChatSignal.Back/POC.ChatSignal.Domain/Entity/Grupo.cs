using POC.ChatSignal.Domain.Entity.Base;

namespace POC.ChatSignal.Domain.Entity
{
    public class Grupo : EntityBase
    {
        public required string Nome { get; set; }
        public required string Usuarios { get; set; }
    }
}
