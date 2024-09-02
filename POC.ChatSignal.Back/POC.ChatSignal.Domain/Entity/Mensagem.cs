using POC.ChatSignal.Domain.Entity.Base;

namespace POC.ChatSignal.Domain.Entity
{
    public class Mensagem : EntityBase
    {
        public required string NomeGrupo { get; set; }
        public required string Usuario { get; set; }
        public required string Texto { get; set; }
        public required DateTime DataCriacao { get; set; }
    }
}
