using FluentValidation;
using POC.ChatSignal.Domain.ViewModel.Hub.Chat.Request;

namespace POC.ChatSignal.Service.Validators.Chat
{
    public class CriarGrupoUsuarioRequestValidator : AbstractValidator<CriarGrupoUsuarioRequest>
    {
        public CriarGrupoUsuarioRequestValidator()
        {
            RuleFor(request => request.EmailConversa).NotNull().NotEmpty().WithMessage("Email da conversa deve ser informado");
            RuleFor(request => request.EmailLogado).NotNull().NotEmpty().WithMessage("Email do usuario loagado deve ser informado");
        }
    }
}
