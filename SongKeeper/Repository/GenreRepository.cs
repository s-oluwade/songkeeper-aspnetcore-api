using SongKeeper.Data;
using SongKeeper.Interfaces;
using SongKeeper.Models;

namespace SongKeeper.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _dataContext;
        public GenreRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateGenre(Genre genre)
        {
            _dataContext.Remove(genre);
            return Save();
        }

        public bool DeleteGenre(Genre genre)
        {
            _dataContext.Remove(genre);
            return Save();
        }

        public bool DeleteGenres(List<Genre> genres)
        {
            _dataContext.RemoveRange(genres);
            return Save();
        }

        public bool GenreExists(int id)
        {
            return _dataContext.Artists.Any(a => a.Id == id);
        }

        public Genre GetGenre(int id)
        {
            return _dataContext.Genres.Where(g => g.Id == id).FirstOrDefault();
        }

        public ICollection<Genre> GetGenres()
        {
            return _dataContext.Genres.OrderBy(a => a.Id).ToList();
        }

        public ICollection<Album> GetAlbums(int genreId)
        {
            return (ICollection<Album>)_dataContext.Genres.Where(g => g.Id ==  genreId).Select(g => g.AlbumGenres.Select(ag => ag.Album)).FirstOrDefault();
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }

        public bool UpdateGenre(Genre genre)
        {
            _dataContext.Update(genre);
            return Save();
        }
    }
}
