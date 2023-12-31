using SongKeeper.Data;
using SongKeeper.Interfaces;
using SongKeeper.Models;

namespace SongKeeper.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly DataContext _dataContext;
        public FavoriteRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateFavorite(Favorite favorite)
        {
            _dataContext.Add(favorite);
            return Save();
        }

        public bool DeleteFavorite(Favorite favorite)
        {
            _dataContext.Remove(favorite);
            return Save();
        }

        public bool DeleteFavorites(List<Favorite> favorites)
        {
            _dataContext.RemoveRange(favorites);
            return Save();
        }

        public bool FavoriteExists(int id)
        {
            return _dataContext.Artists.Any(a => a.Id == id);
        }

        public Favorite GetFavorite(int id)
        {
            return _dataContext.Favorites.Where(f => f.Id == id).FirstOrDefault();
        }

        public ICollection<Favorite> GetFavorites()
        {
            return _dataContext.Favorites.OrderBy(a => a.Id).ToList();
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }

        public bool UpdateFavorite(Favorite favorite)
        {
            _dataContext.Update(favorite);
            return Save();
        }
    }
}
