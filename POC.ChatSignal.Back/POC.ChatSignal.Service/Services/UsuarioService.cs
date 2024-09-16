using FluentValidation;
using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Domain.Exceptions;
using POC.ChatSignal.Domain.Extensions;
using POC.ChatSignal.Domain.Interfaces.Repository;
using POC.ChatSignal.Domain.Interfaces.Service;
using POC.ChatSignal.Domain.ViewModel.Api.Usuario.Request;
using POC.ChatSignal.Domain.ViewModel.Api.Usuario.Response;

namespace POC.ChatSignal.Service.Services
{
    public class UsuarioService(
        IUsuarioRepository usuarioRepository, 
        IValidator<CadastrarUsuarioRequest> validatorCadastrarUsuarioRequest,
        IValidator<LoginRequest> validatorLoginRequest
    ) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IValidator<CadastrarUsuarioRequest> _validatorCadastrarUsuarioRequest = validatorCadastrarUsuarioRequest;
        private readonly IValidator<LoginRequest> _validatorLoginRequest = validatorLoginRequest;

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            await _validatorLoginRequest.ValidarRequestException<LoginRequest, UsuarioException>(request);

            var usuario = await _usuarioRepository.BuscarPorEmailSenha(request.UsuarioEmail, request.UsuarioSenha);

            if (usuario is null)
                throw new UsuarioException("Usuario nao encontrado.");


            await _usuarioRepository.Atualizar(usuario);

            return new LoginResponse { Usuario = usuario };
        }

        public async Task Criar(CadastrarUsuarioRequest request)
        {
            await _validatorCadastrarUsuarioRequest.ValidarRequestException<CadastrarUsuarioRequest, UsuarioException>(request);

            var usuarioExistente = await _usuarioRepository.BuscarPorEmail(request.UsuarioEmail);

            if (usuarioExistente is not null)
                throw new UsuarioException("Usuario ja cadastrado.");

            var usuario = new Usuario() { Email = request.UsuarioEmail, Nome = request.UsuarioNome, Senha = request.UsuarioSenha };
            await _usuarioRepository.Inserir(usuario);
        }

        public async Task<ObterUsuariosResponse> ObterUsuarios()
            => new ObterUsuariosResponse { Usuarios = await _usuarioRepository.Listar() };
    }
}
