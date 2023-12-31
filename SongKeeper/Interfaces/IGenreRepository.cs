using SongKeeper.Models;

namespace SongKeeper.Interfaces
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetGenres();
        ICollection<Album> GetAlbums(int genreId);
        Genre GetGenre(int id);
        bool GenreExists(int id);
        bool CreateGenre(Genre genre);
        bool UpdateGenre(Genre genre);
        bool DeleteGenre(Genre genre);
        bool DeleteGenres(List<Genre> genres);
        bool Save();
    }
}
