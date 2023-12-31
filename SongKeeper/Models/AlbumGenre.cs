namespace SongKeeper.Models
{
    public class AlbumGenre
    {
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public Album Album { get; set; } = null!;
        public Genre Genre { get; set; } = null!;
    }
}
