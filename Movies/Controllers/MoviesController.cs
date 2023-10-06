using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly DbMoviesContext _baseDatos;

        //constructor
        public MoviesController(DbMoviesContext baseDatos)
        {
            this._baseDatos = baseDatos;
        }

        [HttpGet("GetMoviesByUserId/{idUser}")]
        public async Task<IActionResult> GetMoviesByUserId(int idUser)
        {
            try
            {
                // Consulta para obtener todos los idMovie asociados a un idUser específico.
                var idMovies = await _baseDatos.ListFavorites
                    .Where(favorite => favorite.IdUser == idUser)
                    .Select(favorite => favorite.IdMovie)
                    .ToListAsync();

                return Ok(idMovies);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpPost("AddFavorite")]
        public async Task<IActionResult> AddFavorite([FromBody] AddFavoriteRequest request)
        {
            try
            {
                // Verifica si ya existe una entrada con el mismo id_user e id_movie.
                var favoritoExistente = await _baseDatos.ListFavorites.FindAsync(request.IdUser, request.IdMovie);

                if (favoritoExistente != null)
                {
                    return BadRequest("Ya existe un favorito con el mismo id_user e id_movie.");
                }
                else
                {

                // Crea una nueva instancia de ListFavorite y asigna los valores.
                var nuevoFavorito = new ListFavorite
                {
                    IdUser = request.IdUser,
                    IdMovie = request.IdMovie
                };

                // Agrega el nuevo favorito a la tabla ListFavorites.
                _baseDatos.ListFavorites.Add(nuevoFavorito);
                await _baseDatos.SaveChangesAsync();

                return Ok();
                }

            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("RemoveFavorite/{idUser}/{idMovie}")]
        public async Task<IActionResult> RemoveFavorite(int idUser, int idMovie)
        {
            try
            {
                // Busca la entrada en la tabla ListFavorites con el id_user e id_movie específicos.
                var favoritoExistente = await _baseDatos.ListFavorites.FindAsync(idUser, idMovie);

                if (favoritoExistente == null)
                {
                    return NotFound("No se encontró el favorito para eliminar.");
                }

                // Elimina el favorito de la tabla ListFavorites.
                _baseDatos.ListFavorites.Remove(favoritoExistente);
                await _baseDatos.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }


    public class AddFavoriteRequest
    {
        public int IdUser { get; set; }
        public int IdMovie { get; set; }
    }
}

