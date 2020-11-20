namespace MovieShop.Core.Entities
{
    public class MovieGenre
    {
        public int Movieid { get; set; }
        public int Genreid { get; set; }
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}