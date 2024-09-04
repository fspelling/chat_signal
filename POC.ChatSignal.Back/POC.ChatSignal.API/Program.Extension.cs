using Microsoft.EntityFrameworkCore;
using POC.ChatSignal.API.Hubs;
using POC.ChatSignal.Domain.Interfaces.Repository;
using POC.ChatSignal.Domain.Interfaces.Service;
using POC.ChatSignal.Service;
using POC.ChatSignal.Sql;
using POC.ChatSignal.Sql.Repository;

namespace POC.ChatSignal.API
{
    public static class ProgramExtension
    {
        public static void ConfigureInjectDependency(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }

        public static void ConfigureSqlDbContext(this WebApplicationBuilder builder)
            => builder.Services.AddDbContext<ChatDbContext>(db => db.UseSqlite("Data Source=DB\\ChatWeb.db"));

        public static void UseSignalR(this WebApplication app)
            => app.MapHub<ChatHub>("/ChatHub");
    }
}
