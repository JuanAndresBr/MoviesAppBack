namespace Movies.Models
{
    public class MovieResponse
    {
        public List<Movie> Results { get; set; }
    }

    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }



        // Agrega más propiedades según la respuesta JSON de la API de TMDb
    }

}
