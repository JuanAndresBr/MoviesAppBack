using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly DbMoviesContext _baseDatos;

        //constructor
        public LoginController(DbMoviesContext baseDatos)
        {
            this._baseDatos = baseDatos;
        }

        [HttpGet]
        [Route("Usuarios")]
        public async Task<IActionResult> Usuarios()
        {
            var Usuarios = await _baseDatos.Usuarios.ToListAsync();
            return Ok(Usuarios);
        }


        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] Usuario request)
        {
            await _baseDatos.Usuarios.AddAsync(request);
            await _baseDatos.SaveChangesAsync();
            return Ok(request);
        }
    }
}
