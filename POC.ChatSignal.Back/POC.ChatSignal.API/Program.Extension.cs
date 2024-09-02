using Microsoft.EntityFrameworkCore;
using POC.ChatSignal.Sql;

namespace POC.ChatSignal.API
{
    public static class ProgramExtension
    {
        public static void ConfigureInjectDependency(this WebApplicationBuilder builder)
        {
        }

        public static void ConfigureSqlDbContext(this WebApplicationBuilder builder)
            => builder.Services.AddDbContext<ChatDbContext>(db => db.UseSqlite("Data Source=Database\\ChatWeb.db"));
    }
}
