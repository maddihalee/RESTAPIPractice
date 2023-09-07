using RESTAPIPractice.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<RESTAPIPracticeDbContext>(builder.Configuration["RESTAPIPracticeDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Get all artists
app.MapGet("/artists", (RESTAPIPracticeDbContext db) =>
{
    return db.Artists.ToList();
});

// Add an artist
app.MapPost("/artists", (RESTAPIPracticeDbContext db, Artist artist) =>
{
    db.Artists.Add(artist);
    db.SaveChanges();
    return Results.Created($"/artists/{artist.Id}", artist);
});

// Delete an artist
app.MapDelete("/artists/{id}", (RESTAPIPracticeDbContext db, int id) =>
{
    Artist artist = db.Artists.SingleOrDefault(ar => ar.Id == id);
    if (artist == null)
    {
        return Results.NotFound();
    }
    db.Artists.Remove(artist);
    db.SaveChanges();
    return Results.NoContent();
});

// Get artist details
app.MapGet("/artists/{id}", (RESTAPIPracticeDbContext db, int id) =>
{
    Artist artist = db.Artists.SingleOrDefault(ar => ar.Id == id);
    return artist;
});

// Update an artist
app.MapPut("artists/{id}", (RESTAPIPracticeDbContext db, Artist artist, int id) =>
{
    Artist artistToUpdate = db.Artists.SingleOrDefault(ar => ar.Id == id);
    if (artistToUpdate == null)
    {
        return Results.NotFound();
    }
    artistToUpdate.Name = artist.Name;
    artistToUpdate.Age = artist.Age;
    artistToUpdate.Bio = artist.Bio;

    db.SaveChanges();
    return Results.NoContent();
});

// Retrieve a list of a single artist with associated songs
app.MapGet("/artists/{id}/songs", (RESTAPIPracticeDbContext db, int id) =>
{
    Artist artist = db.Artists
    .Include(p => p.Songs)
    .FirstOrDefault(p => p.Id == id);

    return Results.Ok(artist);
});

// Get all genres
app.MapGet("/genres", (RESTAPIPracticeDbContext db) =>
{
    return db.Genres.ToList();
});

// Add a genre
app.MapPost("/genres", (RESTAPIPracticeDbContext db, Genre genre) =>
{
    db.Genres.Add(genre);
    db.SaveChanges();
    return Results.Created($"/genres/{genre.Id}", genre);
});

// Delete a genre
app.MapDelete("/genres/{id}", (RESTAPIPracticeDbContext db, int id) =>
{
    Genre genreToDelete = db.Genres.FirstOrDefault(p => p.Id == id);
    if (genreToDelete == null)
    {
        return Results.NotFound();
    }
    db.Genres.Remove(genreToDelete);
    db.SaveChanges();
    return Results.NoContent();
});

// Update a genre
app.MapPut("/genres/{id}", (RESTAPIPracticeDbContext db, int id, Genre genre) =>
{
    Genre genreToUpdate = db.Genres.FirstOrDefault(gr => gr.Id == id);
    if (genreToUpdate == null)
    {
        return Results.NotFound();
    }
    genreToUpdate.Name = genre.Name;
    genreToUpdate.Description = genre.Description;
    db.SaveChanges();
    return Results.NoContent();
});

// Retrieve details of a single genre with associated songs
app.MapGet("/genres/{id}", (RESTAPIPracticeDbContext db, int id) =>
{
    Genre genre = db.Genres
    .Include(p => p.Songs)
    .FirstOrDefault(p => p.Id == id);

    return Results.Ok(genre);
});

// Get all songs
app.MapGet("/songs", (RESTAPIPracticeDbContext db) =>
{
    return db.Songs.ToList();
});

// Add a song
app.MapPost("/songs", (RESTAPIPracticeDbContext db, Song song) =>
{
    db.Songs.Add(song);
    db.SaveChanges();
    return Results.Created($"/songs/{song.Id}", song);
});

// Delete a song
app.MapDelete("/songs/{id}", (RESTAPIPracticeDbContext db, int id) =>
{
    Song songToDelete = db.Songs.FirstOrDefault(s => s.Id == id);
    if (songToDelete == null)
    {
        return Results.NotFound();
    }
    db.Songs.Remove(songToDelete);
    db.SaveChanges();
    return Results.NoContent();
});

// Update a song
app.MapPut("/songs/{id}", (RESTAPIPracticeDbContext db, int id, Song song) =>
{
    Song songToUpdate = db.Songs.FirstOrDefault(s => s.Id == id);
    if (songToUpdate == null)
    {
        return Results.NotFound();
    }
    songToUpdate.Title = song.Title;
    songToUpdate.Length = song.Length;
    songToUpdate.Album = song.Album;

    db.SaveChanges();
    return Results.NoContent();
});

// Details view of a single song and its associated genres and artist details
app.MapGet("/songs/{id}/artistandgenre", (RESTAPIPracticeDbContext db, int id) =>
{
    var song = db.Songs.Where(s => s.Id == id)
    .Include(p => p.Artist)
    .Include(p => p.Genres).ToList();

    return Results.Ok(song);
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
