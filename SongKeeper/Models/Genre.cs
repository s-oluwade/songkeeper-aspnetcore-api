namespace SongKeeper.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<AlbumGenre> AlbumGenres { get; set; } = new List<AlbumGenre>();
    }
}
