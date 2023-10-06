using System.Net.Http;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Newtonsoft.Json;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "04a6da1ba98d73399b75a9a85bce89e8";

        public MovieController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri("https://api.themoviedb.org/3/");
        }

        [HttpGet]
        [Route("GetMovieById/{movie_id}")]
        public async Task<IActionResult> GetMovieById([FromRoute] string movie_id)
        {
            try
            {
                var endpoint = $"movie/{movie_id}";
                var apiKeyParam = $"?api_key={_apiKey}";


                var response = await _httpClient.GetAsync($"{endpoint}{apiKeyParam}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var movie = JsonConvert.DeserializeObject<Movie>(content);
                    return Ok(movie);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



        [HttpGet]
        [Route("GetMoviesPopular")]
        public async Task<IActionResult> GetMovies()
        {
            var endpoint = "movie/popular"; // Cambia el endpoint según lo que desees obtener
            var apiKeyParam = $"?api_key={_apiKey}";

            var response = await _httpClient.GetAsync($"{endpoint}{apiKeyParam}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var movies = JsonConvert.DeserializeObject<MovieResponse>(content);
                return Ok(movies.Results);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpGet]
        [Route("SearchMovie")]
        public async Task<IActionResult> SearchMovie([FromQuery] string searchTerm)
        {
            try
            {
                var endpoint = "search/movie";
                var apiKeyParam = $"?api_key={_apiKey}";
                var searchTermParam = $"&query={searchTerm}"; // Agrega el término de búsqueda como parámetro de consulta

                var response = await _httpClient.GetAsync($"{endpoint}{apiKeyParam}{searchTermParam}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var movies = JsonConvert.DeserializeObject<MovieResponse>(content);
                    return Ok(movies.Results);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
