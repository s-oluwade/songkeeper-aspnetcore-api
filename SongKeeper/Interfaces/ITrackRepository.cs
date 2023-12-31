using SongKeeper.Models;

namespace SongKeeper.Interfaces
{
    public interface ITrackRepository
    {
        ICollection<Track> GetTracks();
        Track GetTrack(int id);
        bool TrackExists(int id);
        bool CreateTrack(Track track);
        bool UpdateTrack(Track track);
        bool DeleteTrack(Track track);
        bool DeleteTracks(List<Track> tracks);
        bool Save();
    }
}
