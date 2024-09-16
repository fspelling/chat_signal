using FluentValidation;
using POC.ChatSignal.Domain.ViewModel.Hub.Chat.Request;

namespace POC.ChatSignal.Service.Validators.Chat
{
    public class EnviarMensagemRequestValidator : AbstractValidator<EnviarMensagemRequest>
    {
        public EnviarMensagemRequestValidator()
        {
            RuleFor(request => request.UsuarioLogado).NotNull().WithMessage("Usuario logado deve ser informado");
            RuleFor(request => request.NomeGrupo).NotNull().WithMessage("Nome do grupo deve ser informado");
            RuleFor(request => request.Mensagem).NotNull().WithMessage("Mensagem deve ser informada");
        }
    }
}
