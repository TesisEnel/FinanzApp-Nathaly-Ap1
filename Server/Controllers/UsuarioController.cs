using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using FinanzApp.Shared;

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            SesionDTO sesionDTO = new SesionDTO();

            if(login.Correo == "nathaly@gmail.com" && login.Clave == "2021-0126")
            {
                sesionDTO.Nombre = "nathaly";
                sesionDTO.Correo = login.Correo;
                sesionDTO.Rol = "Administrador";
            }
           
            return StatusCode(StatusCodes.Status200OK, sesionDTO);
        }

    }
