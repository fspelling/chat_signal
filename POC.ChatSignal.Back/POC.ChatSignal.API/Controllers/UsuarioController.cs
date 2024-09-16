using Microsoft.AspNetCore.Mvc;
using POC.ChatSignal.API.Controllers.Base;
using POC.ChatSignal.Domain.Exceptions;
using POC.ChatSignal.Domain.Interfaces.Service;
using POC.ChatSignal.Domain.ViewModel.Api.Base;
using POC.ChatSignal.Domain.ViewModel.Api.Usuario.Request;
using POC.ChatSignal.Domain.ViewModel.Api.Usuario.Response;

namespace POC.ChatSignal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(IUsuarioService usuarioService) : BaseController
    {
        private readonly IUsuarioService _usuarioService = usuarioService;

        [HttpGet]
        public async Task<ActionResult<CustomResponseViewModel<ObterUsuariosResponse>>> ObterUsuarios()
        {
            try
            {
                var response = await _usuarioService.ObterUsuarios();
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
    }
}
