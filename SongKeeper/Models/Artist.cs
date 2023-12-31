namespace SongKeeper.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
