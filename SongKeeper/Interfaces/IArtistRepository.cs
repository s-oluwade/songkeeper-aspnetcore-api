using SongKeeper.Models;

namespace SongKeeper.Interfaces
{
    public interface IArtistRepository
    {
        ICollection<Artist> GetArtists();
        ICollection<Album> GetAlbums(int artistId);
        Artist GetArtist(int id);
        bool ArtistExists(int id);
        bool CreateArtist(Artist artist);
        bool UpdateArtist(Artist artist);
        bool DeleteArtist(Artist artist);
        bool DeleteArtists(List<Artist> artists);
        bool Save();
    }
}
