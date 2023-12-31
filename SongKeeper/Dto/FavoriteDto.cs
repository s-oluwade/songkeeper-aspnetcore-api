namespace SongKeeper.Dto
{
    public class FavoriteDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string IdOfType { get; set; } = null!;
        public int UserId { get; set; }
    }
}
