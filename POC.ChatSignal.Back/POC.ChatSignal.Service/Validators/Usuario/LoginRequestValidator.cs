using FluentValidation;
using POC.ChatSignal.Domain.ViewModel.Api.Usuario.Request;

namespace POC.ChatSignal.Service.Validators.Usuario
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(request => request.UsuarioEmail).NotNull().NotEmpty().WithMessage("Email do usuario deve ser informado");
            RuleFor(request => request.UsuarioSenha).NotNull().NotEmpty().WithMessage("Senha do usuario deve ser informado");
        }
    }
}
