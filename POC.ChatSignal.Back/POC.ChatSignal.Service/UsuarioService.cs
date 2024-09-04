using Newtonsoft.Json;
using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Domain.Enums;
using POC.ChatSignal.Domain.Exceptions;
using POC.ChatSignal.Domain.Extensions;
using POC.ChatSignal.Domain.Interfaces.Repository;
using POC.ChatSignal.Domain.Interfaces.Service;
using POC.ChatSignal.Domain.ViewModel.Usuario.Request;
using POC.ChatSignal.Domain.ViewModel.Usuario.Response;

namespace POC.ChatSignal.Service
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {
        public readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var parametrosObrigatorios = new Dictionary<string, object> {
                { nameof(request.UsuarioEmail), request.UsuarioEmail },
                { nameof(request.UsuarioSenha), request.UsuarioSenha }
            };

            parametrosObrigatorios.ValidarCampoObrigatorioException<UsuarioException>();
            var usuario = await _usuarioRepository.BuscarPorEmailSenha(request.UsuarioEmail, request.UsuarioSenha);

            if (usuario is null)
                throw new UsuarioException("Usuario nao encontrado.");


            await _usuarioRepository.Atualizar(usuario);

            return new LoginResponse { Usuario = usuario };
        }

        public async Task Criar(CadastrarUsuarioRequest request)
        {
            var parametrosObrigatorios = new Dictionary<string, object> {
                { nameof(request.UsuarioNome), request.UsuarioNome },
                { nameof(request.UsuarioEmail), request.UsuarioEmail },
                { nameof(request.UsuarioSenha), request.UsuarioSenha }
            };

            parametrosObrigatorios.ValidarCampoObrigatorioException<UsuarioException>();
            var usuarioExistente = await _usuarioRepository.BuscarPorEmail(request.UsuarioEmail);

            if (usuarioExistente is not null)
                throw new UsuarioException("Usuario ja cadastrado.");

            var usuario = new Usuario() { Email = request.UsuarioEmail, Nome = request.UsuarioNome, Senha = request.UsuarioSenha };
            await _usuarioRepository.Inserir(usuario);
        }

        public async Task AtualizarConnection(AtualizarConnectionRequest request)
        {
            var parametrosObrigatorios = new Dictionary<string, object> {
                { nameof(request.UsuarioId), request.UsuarioId },
                { nameof(request.ConnectionId), request.ConnectionId }
            };

            parametrosObrigatorios.ValidarCampoObrigatorioException<UsuarioException>();
            var usuario = await _usuarioRepository.BuscarPorId(request.UsuarioId);

            if (usuario is null)
                throw new UsuarioException("Usuario nao encontrado.");

            usuario.ConnectionIds = SerializarConnections(usuario.ConnectionIds, request.ConnectionId, AcaoConnectionEnum.Adicionar);
            await _usuarioRepository.Atualizar(usuario);
        }
        public async Task RemoverConnection(int usuarioId, string connectionId)
        {
            var parametrosObrigatorios = new Dictionary<string, object> {
                { nameof(usuarioId), usuarioId },
                { nameof(connectionId), connectionId }
            };

            parametrosObrigatorios.ValidarCampoObrigatorioException<UsuarioException>();
            var usuario = await _usuarioRepository.BuscarPorId(usuarioId);

            if (usuario is null)
                throw new UsuarioException("Usuario nao encontrado.");

            usuario.ConnectionIds = SerializarConnections(usuario.ConnectionIds, connectionId, AcaoConnectionEnum.Remover);
            await _usuarioRepository.Atualizar(usuario);
        }

        #region METODOS AUXILIARES

        private string SerializarConnections(string? connections, string ConnectionAtual, AcaoConnectionEnum acao)
        {
            var connectionList = connections is null ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(connections);

            if (!connectionList!.Contains(ConnectionAtual) && acao == AcaoConnectionEnum.Adicionar)
                connectionList.Add(ConnectionAtual);

            if (connectionList.Contains(ConnectionAtual) && acao == AcaoConnectionEnum.Remover)
                connectionList.Remove(ConnectionAtual);

            return JsonConvert.SerializeObject(connectionList);
        }

        #endregion
    }
}
