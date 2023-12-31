namespace SongKeeper.Dto
{
    public class TrackDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public int GenreId { get; set; }
    }
}
