using FluentValidation;
using Newtonsoft.Json;
using POC.ChatSignal.Domain.Entity;
using POC.ChatSignal.Domain.Enums;
using POC.ChatSignal.Domain.Exceptions;
using POC.ChatSignal.Domain.Extensions;
using POC.ChatSignal.Domain.Interfaces.Repository;
using POC.ChatSignal.Domain.Interfaces.Service;
using POC.ChatSignal.Domain.ViewModel.Hub.Chat.Request;
using POC.ChatSignal.Domain.ViewModel.Hub.Chat.Response;
using POC.ChatSignal.Domain.ViewModel.Models;

namespace POC.ChatSignal.Service.Services
{
    public class ChatService(
        IUsuarioRepository usuarioRepository, 
        IGrupoRepository grupoRepository, 
        IMensagemRepository mensagemRepository,
        IValidator<AtualizarConnectionRequest> validatorAtualizarConnectionRequest,
        IValidator<RemoverConnectionRequest> validatorRemoverConnectionRequest,
        IValidator<CriarGrupoUsuarioRequest> validatorCriarGrupoUsuarioRequest,
        IValidator<EnviarMensagemRequest> validatorEnviarMensagemRequest
    ) : IChatService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IGrupoRepository _grupoRepository = grupoRepository;
        private readonly IMensagemRepository _mensagemRepository = mensagemRepository;

        private readonly IValidator<AtualizarConnectionRequest> _validatorAtualizarConnectionRequest = validatorAtualizarConnectionRequest;
        private readonly IValidator<RemoverConnectionRequest> _validatorRemoverConnectionRequest = validatorRemoverConnectionRequest;
        private readonly IValidator<CriarGrupoUsuarioRequest> _validatorCriarGrupoUsuarioRequest = validatorCriarGrupoUsuarioRequest;
        private readonly IValidator<EnviarMensagemRequest> _validatorEnviarMensagemRequest = validatorEnviarMensagemRequest;

        public async Task<AtualizarConnectionResponse> AtualizarConnection(AtualizarConnectionRequest request)
        {
            await _validatorAtualizarConnectionRequest.ValidarRequestException<AtualizarConnectionRequest, ChatException>(request);

            var usuario = await _usuarioRepository.BuscarPorId(request.UsuarioId);

            if (usuario is null)
                throw new ChatException("Usuario nao encontrado.");

            usuario.ConnectionIds = SerializarConnections(usuario.ConnectionIds, request.ConnectionId, AcaoConnectionEnum.Adicionar);
            usuario.IsOnline = true;

            await _usuarioRepository.Atualizar(usuario);

            var gruposConnections = await CarregarGrupoConnectionsPorEmail(usuario.Email);

            return new AtualizarConnectionResponse()
            {
                Usuario = usuario,
                GruposConections = gruposConnections
            };
        }

        public async Task<RemoverConnectionResponse> RemoverConnection(RemoverConnectionRequest request)
        {
            await _validatorRemoverConnectionRequest.ValidarRequestException<RemoverConnectionRequest, ChatException>(request);

            var usuario = await _usuarioRepository.BuscarPorId(request.UsuarioId);

            if (usuario is null)
                throw new ChatException("Usuario nao encontrado.");

            usuario.ConnectionIds = SerializarConnections(usuario.ConnectionIds, request.ConnectionId, AcaoConnectionEnum.Remover);
            usuario.IsOnline = !string.IsNullOrWhiteSpace(usuario.ConnectionIds) && usuario.ConnectionIds != "[]";

            await _usuarioRepository.Atualizar(usuario);

            var gruposConnections = await CarregarGrupoConnectionsPorEmail(usuario.Email);

            return new RemoverConnectionResponse()
            {
                Usuario = usuario,
                GruposConections = gruposConnections
            };
        }

        public async Task<CriarGrupoUsuarioResponse> CriarGrupoUsuario(CriarGrupoUsuarioRequest request)
        {
            await _validatorCriarGrupoUsuarioRequest.ValidarRequestException<CriarGrupoUsuarioRequest, ChatException>(request);

            var emailsGrupo = new List<string>() { request.EmailLogado, request.EmailConversa }.OrderBy(email => email).ToList();
            var nomeGrupo = string.Join("-", emailsGrupo);
            var grupo = await _grupoRepository.BuscarPorNome(string.Join("-", emailsGrupo));

            if (grupo is null)
                await _grupoRepository.Inserir(new Grupo() { Nome = nomeGrupo, Usuarios = JsonConvert.SerializeObject(emailsGrupo) });

            return new CriarGrupoUsuarioResponse()
            {
                Mensagens = await _mensagemRepository.ListarPorNomeGrupo(nomeGrupo),
                GrupoConections = new GrupoConections()
                {
                    GrupoNome = nomeGrupo,
                    ConectionIds = await CarregarConnectionsPorEmails(emailsGrupo!)
                }
            };
        }

        public async Task<EnviarMensagemResponse> EnviarMensagem(EnviarMensagemRequest request)
        {
            await _validatorEnviarMensagemRequest.ValidarRequestException<EnviarMensagemRequest, ChatException>(request);

            var grupo = await _grupoRepository.BuscarPorNome(request.NomeGrupo);

            if (!grupo!.Usuarios.Contains(request.UsuarioLogado.Email))
                throw new ChatException("Usuario nao pertence ao grupo.");

            var mensagem = new Mensagem()
            {
                Usuario = JsonConvert.SerializeObject(request.UsuarioLogado),
                NomeGrupo = grupo.Nome,
                Texto = request.Mensagem,
                DataCriacao = DateTime.Now
            };

            await _mensagemRepository.Inserir(mensagem);
            return new EnviarMensagemResponse() { Mensagem = mensagem };
        }

        #region METODOS AUXILIARES

        private string SerializarConnections(string? connections, string ConnectionAtual, AcaoConnectionEnum acao)
        {
            var connectionList = string.IsNullOrEmpty(connections) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(connections);

            if (!connectionList!.Contains(ConnectionAtual) && acao == AcaoConnectionEnum.Adicionar)
                connectionList.Add(ConnectionAtual);

            if (connectionList.Contains(ConnectionAtual) && acao == AcaoConnectionEnum.Remover)
                connectionList.Remove(ConnectionAtual);

            return JsonConvert.SerializeObject(connectionList);
        }

        private async Task<List<string>> CarregarConnectionsPorEmails(List<string> emails)
        {
            var conectionIds = new List<string>();

            foreach (var emailUsuario in emails)
            {
                var usuario = await _usuarioRepository.BuscarPorEmail(emailUsuario);

                if (usuario!.ConnectionIds is not null)
                {
                    var conections = JsonConvert.DeserializeObject<List<string>>(usuario!.ConnectionIds!);
                    conectionIds.AddRange(conections!);
                }
            }

            return conectionIds;
        }

        private async Task<List<GrupoConections>> CarregarGrupoConnectionsPorEmail(string email)
        {
            var grupoConections = new List<GrupoConections>();
            var gruposUsuario = await _grupoRepository.ObterPorUsuario(email);

            foreach (var grupo in gruposUsuario)
            {
                grupoConections.Add(new GrupoConections()
                {
                    GrupoNome = grupo.Nome,
                    ConectionIds = await CarregarConnectionsPorEmails(new List<string>() { email })
                });
            }

            return grupoConections;
        }

        #endregion
    }
}
