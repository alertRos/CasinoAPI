using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using CasinoAPI.Data;
using CasinoAPI.Models.Dto;

namespace CasinoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Casino : ControllerBase
    {
        private readonly ILogger<Casino> _logger;

        public Casino(ILogger<Casino> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Route("Usuario")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            _logger.LogInformation("Obteniendo usuarios");
            DataStudio User = new DataStudio();

            var list = await User.ShowUsers();

            if (list == null) return StatusCode(400);

            return StatusCode(200, list);
        }

        [HttpGet]
        [Route("Usuario/{id}", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<User>>> GetUserById(int id)
        {
            DataStudio User = new DataStudio();

            var list = User.ShowUserById(id);

            if(list.Count == 0)
            {
                return NotFound("Usuario no encontrado");
            }

            return list;
        }

        [HttpPut]
        [Route("Usuario/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] User user)
        {
            DataStudio dataStudio = new DataStudio();
            user.id = id;

            await dataStudio.EditUser(user);

            return NoContent();
        }

        [HttpPost]
        [Route("Usuario")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            DataStudio dataStudio = new DataStudio();
            if(!ModelState.IsValid)
            {
                StatusCode(400, ModelState);
            }
            
            var list = await dataStudio.ShowUsers();
            var validacion = list.FirstOrDefault(c=>c.name.Trim().ToLower() == user.name.Trim().ToLower());
            
            if ( validacion != null)
            {
                ModelState.AddModelError("UsuarioExiste", "El usuario ya existe");
                return BadRequest(ModelState);
            }
            if(user == null)
            {
                return StatusCode(400, user);
            }

            DataStudio data = new DataStudio();
            await data.CreateUser(user);

            return CreatedAtRoute("GetUsuario", new {id = user.id},user);

        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            DataStudio data = new DataStudio();
            await data.DeleteUser(id);
        }


    }
}
