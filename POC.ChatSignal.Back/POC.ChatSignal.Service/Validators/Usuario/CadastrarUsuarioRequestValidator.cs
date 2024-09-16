using FluentValidation;
using POC.ChatSignal.Domain.ViewModel.Api.Usuario.Request;

namespace POC.ChatSignal.Service.Validators.Usuario
{
    public class CadastrarUsuarioRequestValidator : AbstractValidator<CadastrarUsuarioRequest>
    {
        public CadastrarUsuarioRequestValidator()
        {
            RuleFor(request => request.UsuarioNome).NotNull().NotEmpty().WithMessage("Nome do usuario deve ser informado");
            RuleFor(request => request.UsuarioSenha).NotNull().NotEmpty().WithMessage("Senha do usuario deve ser informado");
            RuleFor(request => request.UsuarioEmail).NotNull().NotEmpty().WithMessage("Email do usuario deve ser informado");
        }
    }
}
