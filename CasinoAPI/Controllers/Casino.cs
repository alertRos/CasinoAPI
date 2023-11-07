using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

using CasinoAPI.Data;
using CasinoAPI.Models;

namespace CasinoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Casino : ControllerBase
    {

        [HttpGet]
        [Route("Usuario")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            DataStudio User = new DataStudio();

            var list = await User.ShowUsers();

            if (list == null) return StatusCode(400);

            return StatusCode(200, list);
        }

        [HttpGet]
        [Route("Usuario/{id}")]
        public async Task<ActionResult<List<User>>> GetUserById(int id)
        {
            DataStudio User = new DataStudio();

            var list = await User.ShowUserById(id);

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
        public async Task<Object> Post([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.name) || user.hobbies == null )
            {
                return BadRequest("Datos vacios. Ingrese datos validos.");
            }
            else
            {
                DataStudio data = new DataStudio();
                await data.CreateUser(user);

                return StatusCode(201, "Usuario creado");

            }
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            DataStudio data = new DataStudio();
            await data.DeleteUser(id);
        }


    }
}
