namespace Bookstore.Models;

public class Book
{
    public int Id { get; set; }
    public BookInfo? Info { get; set; }
    public string Title { get; set; } = null!;
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!;
}