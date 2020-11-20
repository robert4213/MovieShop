namespace MovieShop.Core.Entities
{
    //One Movie Can Have Multi Trailers
    public class Trailer
    {
        public int Id { get; set; }
        // Foreign Key From Table
        public int MovieId { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }
        // Navigation Prop, help to navigate related entities
        public Movie Movie { get; set; }
    }
}