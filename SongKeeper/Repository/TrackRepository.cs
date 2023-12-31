using SongKeeper.Data;
using SongKeeper.Interfaces;
using SongKeeper.Models;

namespace SongKeeper.Repository
{
    public class TrackRepository : ITrackRepository
    {
        private readonly DataContext _dataContext;
        public TrackRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool CreateTrack(Track track)
        {
            _dataContext.Add(track);
            return Save();
        }

        public bool DeleteTrack(Track track)
        {
            _dataContext.Remove(track);
            return Save();
        }

        public bool DeleteTracks(List<Track> tracks)
        {
            _dataContext.RemoveRange(tracks);
            return Save();
        }

        public Track GetTrack(int id)
        {
            return _dataContext.Tracks.Where(s => s.Id == id).FirstOrDefault();
        }
        public Track GetTrack(string title)
        {
            return _dataContext.Tracks.Where(s => s.Title == title).FirstOrDefault();
        }

        public ICollection<Track> GetTracks()
        {
            return _dataContext.Tracks.OrderBy(a => a.Id).ToList();
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }

        public bool TrackExists(int id)
        {
            return _dataContext.Tracks.Any(a => a.Id == id);
        }

        public bool UpdateTrack(Track track)
        {
            _dataContext.Update(track);
            return Save();
        }
    }
}
