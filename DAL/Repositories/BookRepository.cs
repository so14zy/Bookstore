using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.DAL.Repositories;

public class BookRepository : IBookRepository
{
    DatabaseApplicationContext _db;

    public BookRepository(DatabaseApplicationContext db)
    {
        _db = db;
    }

    public async void add(Book book)
    {
        Book? tempBook = await getById(book.Id);
        if (tempBook == null)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
        }
    }

    public async Task<Book> getById(int id)
    {
        Book? book = await _db.Books
            .Where(u => u.Id == id)
            .Include(u => u.Info)
            .FirstOrDefaultAsync();

        return book;
    }

    public async Task<Book> getByIdIncluded(int id)
    {
        Book? book = await _db.Books
            .Where(u => u.Id == id)
            .Include(u => u.Info)
            .ThenInclude(u => u.Language)
            .Include(u => u.Author)
            .Include(u => u.Genre)
            .FirstOrDefaultAsync();

        return book;
    }

    public async Task<List<Book>> getAll()
    {
        List<Book> books = await _db.Books
            .Include(u => u.Info)
            .OrderBy(u => u.Title)
            .ToListAsync();

        return books;
    }

    public async Task<List<Book>> getAllIncluded()
    {
        List<Book> books = await _db.Books
            .Include(u => u.Info)
            .Include(u => u.Author)
            .Include(u => u.Genre)
            .OrderBy(u => u.Title)
            .ToListAsync();

        return books;
    }

    public async void update(Book book)
    {
        Book? tempBook = await getById(book.Id);
        if (tempBook == null)
            _db.Books.Add(book);
        else
        {
            tempBook.AuthorId = book.AuthorId;
            tempBook.GenreId = book.GenreId;
            tempBook.Title = book.Title;
        }

        _db.SaveChanges();
    }

    public async void delete(int id)
    {
        Book? book = await getById(id);
        if (book != null)
        {
            _db.Books.Remove(book);
            _db.SaveChanges();
        }
    }
}