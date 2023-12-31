using SongKeeper.Data;
using SongKeeper.Models;
using System.Security.AccessControl;

namespace SongKeeper
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Users.Any())
            {
                var users = new List<User>()
                {
                    new User()
                    {
                        Username = "musiclover123",
                        Email = "musiclover@example.com",
                        Favorites = new List<Favorite>()
                    },
                };

                var artists = new List<Artist>()
                {
                    new Artist()
                    {
                        Name = "The Rhythmic Beats",
                        Albums = new List<Album>()
                        {
                            new Album()
                            {
                                Title = "Harmony of Seasons",
                                Tracks = new List<Track>()
                                {
                                    new Track()
                                    {
                                        Title = "Dancing in the Rain",
                                        Duration = "4:15",
                                        GenreId = 1
                                    },
                                    new Track()
                                    {
                                        Title = "Sunset Serenade",
                                        Duration = "3:50",
                                        GenreId = 4
                                    }
                                }
                            },
                            new Album()
                            {
                                Title = "Dance Fusion",
                                Tracks = new List<Track>()
                                {
                                    new Track()
                                    {
                                        Title = "Electric Dreams",
                                        Duration = "5:10",
                                        GenreId = 3
                                    }
                                }
                            },
                        }
                    }
                };

                var genres = new List<Genre>()
                {
                    new Genre()
                    {
                        Name = "Pop"
                    },
                    new Genre()
                    {
                        Name = "Rock"
                    },
                    new Genre()
                    {
                        Name = "Electronic"
                    },
                    new Genre()
                    {
                        Name = "R&B"
                    }
                };

                dataContext.Users.AddRange(users);
                dataContext.Artists.AddRange(artists);
                dataContext.Genres.AddRange(genres);
                dataContext.SaveChanges();
            }
        }
    }
}
