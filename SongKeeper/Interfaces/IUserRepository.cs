using SongKeeper.Models;
using System.Security.AccessControl;

namespace SongKeeper.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUser(string name);
        ICollection<Favorite> GetFavorites(int id);
        bool UserExists(int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool DeleteUsers(List<User> users);
        bool Save();
    }
}
