using Microsoft.AspNetCore.Mvc;
using POC.ChatSignal.API.Controllers.Base;
using POC.ChatSignal.Domain.Interfaces.Service;

namespace POC.ChatSignal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(IUsuarioService usuarioService) : BaseController
    {
        private readonly IUsuarioService _usuarioService = usuarioService;
    }
}
