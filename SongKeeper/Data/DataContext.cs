using Microsoft.EntityFrameworkCore;
using SongKeeper.Models;

namespace SongKeeper.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<AlbumGenre> AlbumGenres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ties our many-to-many relationship
            modelBuilder.Entity<AlbumGenre>()
                .HasKey(ag => new { ag.GenreId, ag.AlbumId });
            modelBuilder.Entity<AlbumGenre>()
                .HasOne(a => a.Album)
                .WithMany(ag => ag.AlbumGenres)
                .HasForeignKey(a => a.AlbumId);
            modelBuilder.Entity<AlbumGenre>()
                .HasOne(g => g.Genre)
                .WithMany(ag => ag.AlbumGenres)
                .HasForeignKey(c => c.GenreId);
        }
    }
}
