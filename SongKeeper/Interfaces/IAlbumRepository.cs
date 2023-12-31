using SongKeeper.Models;

namespace SongKeeper.Interfaces
{
    public interface IAlbumRepository
    {
        ICollection<Album> GetAlbums();
        ICollection<Track> GetTracks(int albumId);
        Album GetAlbum(int id);
        bool AlbumExists(int id);
        bool CreateAlbum(Album album);
        bool UpdateAlbum(Album album);
        bool DeleteAlbum(Album album);
        bool DeleteAlbums(List<Album> albums);
        bool Save();
    }
}
