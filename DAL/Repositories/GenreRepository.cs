using Microsoft.EntityFrameworkCore;
using Bookstore.Models;

namespace Bookstore.DAL.Repositories;


public class GenreRepository : IGenreRepository
{
    DatabaseApplicationContext _db;

    public GenreRepository(DatabaseApplicationContext db)
    {
        _db = db;
    }

    public async void add(Genre genre)
    {
        Genre? tempGenre = await getById(genre.Id);
        if (tempGenre == null)
        {
            _db.Genres.Add(genre);
            _db.SaveChanges();
        }
    }

    public async Task<Genre> getById(int id)
    {
        Genre? genre = await _db.Genres
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        return genre;
    }

    public async Task<Genre> getByIdIncluded(int id)
    {
        Genre? genre = await _db.Genres
            .Where(u => u.Id == id)
            .Include(u => u.Books)
            .FirstOrDefaultAsync();

        return genre;
    }

    public async Task<List<Genre>> getAll()
    {
        List<Genre> genres = await _db.Genres
            .OrderBy(u => u.Name)
            .ToListAsync();

        return genres;
    }

    public async Task<List<Genre>> getAllIncluded()
    {
        List<Genre> genres = await _db.Genres
            .Include(u => u.Books.OrderBy(x => x.Title))
            .OrderBy(u => u.Name)
            .ToListAsync();

        return genres;
    }

     public async void update(Genre genre)
    {
        Genre? tempGenre = await getById(genre.Id);
        if (tempGenre == null)
            _db.Genres.Add(genre);
        else
            tempGenre.Name = genre.Name;

        _db.SaveChanges();
    }

    public async void delete(int id)
    {
        Genre? genre = await getById(id);
        if (genre != null)
        {
            _db.Genres.Remove(genre);
            _db.SaveChanges();
        }
    }
}