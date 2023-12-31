using SongKeeper.Data;
using SongKeeper.Interfaces;
using SongKeeper.Models;

namespace SongKeeper.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly DataContext _dataContext;

        public ArtistRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool ArtistExists(int id)
        {
            return _dataContext.Artists.Any(a => a.Id == id);
        }

        public bool CreateArtist(Artist artist)
        {
            _dataContext.Add(artist);
            return Save();
        }

        public bool DeleteArtist(Artist artist)
        {
            _dataContext.Remove(artist);
            return Save();
        }

        public bool DeleteArtists(List<Artist> artists)
        {
            _dataContext.RemoveRange(artists);
            return Save();
        }

        public bool UpdateArtist(Artist artist)
        {
            _dataContext.Update(artist);
            return Save();
        }

        public Artist GetArtist(int id)
        {
            return _dataContext.Artists.Where(a => a.Id == id).FirstOrDefault();
        }

        public Artist GetArtist(string name)
        {
            return _dataContext.Artists.Where(a => a.Name == name).FirstOrDefault();
        }

        public ICollection<Artist> GetArtists()
        {
            return _dataContext.Artists.OrderBy(a => a.Id).ToList();
        }

        public ICollection<Album> GetAlbums(int artistId)
        {
            return _dataContext.Artists.Where(a => a.Id == artistId).Select(a => a.Albums).FirstOrDefault();
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }
    }
}
