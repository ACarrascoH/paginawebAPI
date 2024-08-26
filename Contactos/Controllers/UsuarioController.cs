using Contactos.Models;
using Contactos.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Contactos.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController:ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ILogger<UsuarioRepository> _logger;

        public UsuarioController(IUsuarioRepository usuarioRepo,
            ILogger<UsuarioRepository> logger)
        {
            _usuarioRepo = usuarioRepo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuario=await _usuarioRepo.ObtenerTodosAsync();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
                throw;
            }
        }

        [HttpGet("{UsuarioPK}")]
        public async Task<IActionResult> Get(int UsuarioPK)
        {
            try
            {
                var usuario = await _usuarioRepo.ObtenerUsuarioAsync(UsuarioPK);
                if(usuario == null)
                {
                    return NotFound(new
                    {
                        StatusCode= 404,
                        message="Usuario no existe"
                    });
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuario usuario)
        {
            try
            {
                var user = await _usuarioRepo.CrearUsuarioAsync(usuario); ;
                
                return CreatedAtAction(nameof(Post), user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Usuario usuario)
        {
            try
            {
                var existe = await _usuarioRepo.ObtenerUsuarioAsync(usuario.UsuarioPK);
                if (existe == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "Usuario no existe"
                    });
                }
                await _usuarioRepo.ActualizarUsuarioAsync(usuario);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
                throw;
            }
        }

        [HttpDelete("{UsuarioPK}")]
        public async Task<IActionResult> Delete(int UsuarioPK)
        {
            try
            {
                var existe = await _usuarioRepo.ObtenerUsuarioAsync(UsuarioPK);
                if (existe == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "Usuario no existe"
                    });
                }
                await _usuarioRepo.EliminarUsuarioAsync(UsuarioPK);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
                throw;
            }
        }






    }
}
