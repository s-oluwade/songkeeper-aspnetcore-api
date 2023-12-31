using Microsoft.EntityFrameworkCore;
using SongKeeper.Data;
using SongKeeper.Interfaces;
using SongKeeper.Models;

namespace SongKeeper.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly DataContext _dataContext;

        public AlbumRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool AlbumExists(int id)
        {
            return _dataContext.Albums.Any(a => a.Id == id);
        }

        public bool CreateAlbum(Album album)
        {
            _dataContext.Add(album);
            return Save();
        }

        public bool DeleteAlbum(Album album)
        {
            _dataContext.Remove(album);
            return Save();
        }

        public bool DeleteAlbums(List<Album> albums)
        {
            _dataContext.RemoveRange(albums);
            return Save();
        }

        public Album GetAlbum(int id)
        {
            return _dataContext.Albums.Where(a => a.Id == id).Include(a => a.Tracks).FirstOrDefault();
        }

        public Album GetAlbum(string title)
        {
            return _dataContext.Albums.Where(a => a.Title == title).Include(a => a.Tracks).FirstOrDefault();
        }

        public ICollection<Album> GetAlbums()
        {
            return _dataContext.Albums.OrderBy(a => a.Id).Include(a => a.Artist).Include(a => a.Tracks).ToList();
        }

        public ICollection<Track> GetTracks(int albumId)
        {
            return _dataContext.Albums.Where(a => a.Id == albumId).Select(a => a.Tracks).FirstOrDefault();
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }

        public bool UpdateAlbum(Album album)
        {
            _dataContext.Update(album);
            return Save();
        }
    }
}
