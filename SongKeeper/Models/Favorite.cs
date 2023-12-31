namespace SongKeeper.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string IdOfType { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
