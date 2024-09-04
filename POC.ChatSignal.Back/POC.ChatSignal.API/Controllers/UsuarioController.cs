using Microsoft.AspNetCore.Mvc;
using POC.ChatSignal.API.Controllers.Base;
using POC.ChatSignal.Domain.Exceptions;
using POC.ChatSignal.Domain.Interfaces.Service;
using POC.ChatSignal.Domain.ViewModel.Base;
using POC.ChatSignal.Domain.ViewModel.Usuario.Request;
using POC.ChatSignal.Domain.ViewModel.Usuario.Response;

namespace POC.ChatSignal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(IUsuarioService usuarioService) : BaseController
    {
        private readonly IUsuarioService _usuarioService = usuarioService;

        [HttpPost("signin")]
        public async Task<ActionResult<CustomResponseViewModel<LoginResponse>>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _usuarioService.Login(request);
                return CustomResponse(response);
            }
            catch (UsuarioException e)
            {
                return CustomResponseError(System.Net.HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return CustomResponseError(System.Net.HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPost("signup")]
        public async Task<ActionResult<CustomResponseViewModel<object>>> CadastrarUsuario([FromBody]CadastrarUsuarioRequest request)
        {
            try
            {
                await _usuarioService.Criar(request);
                return CustomResponse<object>(null);
            }
            catch (UsuarioException e)
            {
                return CustomResponseError(System.Net.HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return CustomResponseError(System.Net.HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPatch("connection")]
        public async Task<ActionResult<CustomResponseViewModel<object>>> AtualizarConnection([FromBody] AtualizarConnectionRequest request)
        {
            try
            {
                await _usuarioService.AtualizarConnection(request);
                return CustomResponse<object>(null);
            }
            catch (UsuarioException e)
            {
                return CustomResponseError(System.Net.HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return CustomResponseError(System.Net.HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpDelete("{usuarioId}/connection/{connectionId}")]
        public async Task<ActionResult<CustomResponseViewModel<object>>> RemoverConnection(int usuarioId, string connectionId)
        {
            try
            {
                await _usuarioService.RemoverConnection(usuarioId, connectionId);
                return CustomResponse<object>(null);
            }
            catch (UsuarioException e)
            {
                return CustomResponseError(System.Net.HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                return CustomResponseError(System.Net.HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
