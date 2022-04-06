using DevelSystem.Helpers;
using DevelSystem.Models;
using DevelSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevelSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class EncuestaController : ControllerBase
    {
        private IEncuestaService _encuestaService;

        public EncuestaController(IEncuestaService encuestaService)
        {
            _encuestaService = encuestaService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _encuestaService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }


        [Authorize]
        [HttpPost]
        [Route("NuevaEncuesta")]
        public IActionResult NuevaEncuesta(Encuesta encuesta)
        {
            var response = _encuestaService.NuevaEncuesta(encuesta);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        [Route("EditarEncuesta")]
        public IActionResult EditarEncuesta(Encuesta encuesta)
        {
            var response = _encuestaService.EditarEncuesta(encuesta);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("EliminarEncuesta/{idEncuesta}")]
        public IActionResult EliminarEncuesta(int idEncuesta)
        {
            var response = _encuestaService.EliminarEncuesta(idEncuesta);
            return Ok(response);
        }

    }
}
