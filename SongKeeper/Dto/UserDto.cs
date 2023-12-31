using SongKeeper.Models;

namespace SongKeeper.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
