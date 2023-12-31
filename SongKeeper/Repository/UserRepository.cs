using SongKeeper.Data;
using SongKeeper.Interfaces;
using SongKeeper.Models;

namespace SongKeeper.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool CreateUser(User user)
        {
            _dataContext.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _dataContext.Remove(user);
            return Save();
        }

        public bool DeleteUsers(List<User> users)
        {
            _dataContext.RemoveRange(users);
            return Save();
        }

        public User GetUser(int id)
        {
            return _dataContext.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User GetUser(string name)
        {
            return _dataContext.Users.Where(u => u.Username.ToUpper() == name.Trim().ToUpper()).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _dataContext.Users.OrderBy(a => a.Id).ToList();
        }

        public ICollection<Favorite> GetFavorites(int id)
        {
            return _dataContext.Users.Where(u => u.Id == id).Select(u => u.Favorites).FirstOrDefault();
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }

        public bool UpdateUser(User user)
        {
            _dataContext.Update(user);
            return Save();
        }

        public bool UserExists(int id)
        {
            return _dataContext.Users.Any(a => a.Id == id);
        }
    }
}
