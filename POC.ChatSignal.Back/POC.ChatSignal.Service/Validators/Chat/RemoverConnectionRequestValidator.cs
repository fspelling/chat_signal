using FluentValidation;
using POC.ChatSignal.Domain.ViewModel.Hub.Chat.Request;

namespace POC.ChatSignal.Service.Validators.Chat
{
    public class RemoverConnectionRequestValidator : AbstractValidator<RemoverConnectionRequest>
    {
        public RemoverConnectionRequestValidator()
        {
            RuleFor(request => request.ConnectionId).NotNull().NotEmpty().WithMessage("Id da conexao signalr deve ser informado");
            RuleFor(request => request.UsuarioId).NotNull().NotEmpty().WithMessage("Id do usuario deve ser informado");
        }
    }
}
