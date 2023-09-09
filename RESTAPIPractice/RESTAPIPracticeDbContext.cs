using Microsoft.EntityFrameworkCore;

namespace RESTAPIPractice.Models;

  

    public class RESTAPIPracticeDbContext : DbContext
    {
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Song> Songs { get; set; }

    public RESTAPIPracticeDbContext(DbContextOptions<RESTAPIPracticeDbContext> context) : base(context) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Artist>().HasData(new Artist[]
        {
            new Artist {Id = 1, Name = "Ben Gibbard", Age = 47, Bio = "Lead singer of Death Cab for Cutie"},
            new Artist {Id = 2, Name = "Lars Karlsson", Age = 48, Bio = "Lead of both Galantis and Miike Snow"},
            new Artist {Id = 3, Name = "Jesse Kardon", Age = 30, Bio = "Also known as Subtronics"},
            new Artist {Id = 4, Name = "Damian Kulash", Age = 47, Bio = "Lead singer of OK Go"}
        });

        modelBuilder.Entity<Genre>().HasData(new Genre[]
        {
            new Genre {Id = 123, Name = "Indie", Description = "Indie music"},
            new Genre {Id = 124, Name = "Dubstep", Description = "A genre of EDM"},
            new Genre {Id = 125, Name = "Pop", Description = "Popular music"},
            new Genre {Id = 126, Name = "Rock", Description = "a popular genre of music that consists of many sub-genres"}
        });

        modelBuilder.Entity<Song>().HasData(new Song[]
        {
            new Song {Id = 234, Title = "The Ghosts of Beverly Drive", ArtistId = 1, Album = "Kintsugi", Length = "4:14"},
            new Song {Id = 235, Title = "Animal", ArtistId = 2, Album = "Miike Snow", Length = "4:24"},
            new Song {Id = 236, Title = "Griztronics", ArtistId = 3, Album = "Bangers[2].Zip", Length = "3:18"},
            new Song {Id = 237, Title = "Get Over It", ArtistId = 4, Album = "OK Go", Length = "3:17"}
        });

        var genreSong = modelBuilder.Entity("GenreSong");
        genreSong.HasData(
            new { GenresId = 123, SongsId = 234},
            new { GenresId = 124, SongsId = 236},
            new { GenresId = 125, SongsId = 235},
            new { GenresId = 125, SongsId = 237});
    }
    }

