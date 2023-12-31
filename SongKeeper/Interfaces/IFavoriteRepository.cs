using SongKeeper.Models;

namespace SongKeeper.Interfaces
{
    public interface IFavoriteRepository
    {
        ICollection<Favorite> GetFavorites();
        Favorite GetFavorite(int id);
        bool FavoriteExists(int id);
        bool CreateFavorite(Favorite favorite);
        bool UpdateFavorite(Favorite favorite);
        bool DeleteFavorite(Favorite favorite);
        bool DeleteFavorites(List<Favorite> favorites);
        bool Save();
    }
}
