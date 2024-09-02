using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POC.ChatSignal.Domain.Entity;

namespace POC.ChatSignal.Sql.EntityConfiguration
{
    internal class MensagemConfiguration : IEntityTypeConfiguration<Mensagem>
    {
        public void Configure(EntityTypeBuilder<Mensagem> builder)
        {
        }
    }
}
