namespace SongKeeper.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int ArtistId { get; set; }
        public Artist Artist { get; set; } = null!;
        public ICollection<Track> Tracks { get; set; } = new List<Track>();
        public ICollection<AlbumGenre> AlbumGenres { get; set; } = new List<AlbumGenre>();
    }
}
