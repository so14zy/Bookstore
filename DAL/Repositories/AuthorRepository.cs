using Microsoft.EntityFrameworkCore;
using Bookstore.Models;

namespace Bookstore.DAL.Repositories;

public class AuthorRepository : IAuthorRepository
{
    DatabaseApplicationContext _db;

    public AuthorRepository(DatabaseApplicationContext db)
    {
        _db = db;
    }

    public async void add(Author author)
    {
        Author? tempAuthor = await getById(author.Id);
        if (tempAuthor == null)
        {
            _db.Authors.Add(author);
            _db.SaveChanges();
        }
    }

    public async Task<Author> getById(int id)
    {
        
        Author? author = await _db.Authors
            .Where(u => u.Id == id)
            .Include(u => u.Info)
            .ThenInclude(u => u.Country)
            .FirstOrDefaultAsync();

        return author;
    }

    public async Task<Author> getByIdIncluded(int id)
    {
        Author? author = await _db.Authors
            .Where(u => u.Id == id)
            .Include(u => u.Info)
            .ThenInclude(u => u.Country)
            .Include(u => u.Books)
            .FirstOrDefaultAsync();

        return author;
    }

    public async Task<List<Author>> getAll()
    {
        List<Author> authors = await _db.Authors
            .Include(u => u.Info)
            .ThenInclude(u => u.Country)
            .OrderBy(u => u.SecondName)
            .ToListAsync();
            
        return authors;
    }

    public async Task<List<Author>> getAllIncluded()
    {
        List<Author> authors = await _db.Authors
            .Include(u => u.Info)
            .ThenInclude(u => u.Country)
            .Include(u => u.Books.OrderBy(u => u.Title))
            .OrderBy(u => u.SecondName)
            .ToListAsync();

        return authors;
    }

    public async void update(Author author)
    {
        Author? tempAuthor = await getById(author.Id);
        if (tempAuthor == null)
            _db.Authors.Add(author);
        else
        {
            tempAuthor.FirstName = author.FirstName;
            tempAuthor.SecondName = author.SecondName;
        }
        
        _db.SaveChanges();
    }

    public async void delete(int id)
    {
        Author? author = await getById(id);
        if (author != null)
        {
            _db.Authors.Remove(author);
            _db.SaveChanges();
        }
    }
}