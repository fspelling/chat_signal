using FluentValidation;
using Microsoft.EntityFrameworkCore;
using POC.ChatSignal.API.Hubs;
using POC.ChatSignal.Domain.Interfaces.Repository;
using POC.ChatSignal.Domain.Interfaces.Service;
using POC.ChatSignal.Domain.ViewModel.Api.Usuario.Request;
using POC.ChatSignal.Domain.ViewModel.Hub.Chat.Request;
using POC.ChatSignal.Service.Services;
using POC.ChatSignal.Service.Validators.Chat;
using POC.ChatSignal.Service.Validators.Usuario;
using POC.ChatSignal.Sql;
using POC.ChatSignal.Sql.Repository;

namespace POC.ChatSignal.API
{
    public static class ProgramExtension
    {
        public static void ConfigureInjectDependency(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IChatService, ChatService>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IGrupoRepository, GrupoRepository>();
            builder.Services.AddScoped<IMensagemRepository, MensagemRepository>();
        }

        public static void ConfigureSqlDbContext(this WebApplicationBuilder builder)
            => builder.Services.AddDbContext<ChatDbContext>(db => db.UseSqlite("Data Source=DB\\ChatWeb.db"));

        public static void UseSignalR(this WebApplication app)
            => app.MapHub<ChatHub>("/ChatHub");

        public static void ConfigureValidators(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<CadastrarUsuarioRequest>, CadastrarUsuarioRequestValidator>();
            builder.Services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();

            builder.Services.AddScoped<IValidator<AtualizarConnectionRequest>, AtualizarConnectionRequestValidator>();
            builder.Services.AddScoped<IValidator<CriarGrupoUsuarioRequest>, CriarGrupoUsuarioRequestValidator>();
            builder.Services.AddScoped<IValidator<EnviarMensagemRequest>, EnviarMensagemRequestValidator>();
            builder.Services.AddScoped<IValidator<RemoverConnectionRequest>, RemoverConnectionRequestValidator>();
        }
    }
}
