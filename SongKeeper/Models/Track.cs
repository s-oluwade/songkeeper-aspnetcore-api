namespace SongKeeper.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public Album Album { get; set; } = null!;
    }
}
