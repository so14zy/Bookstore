namespace Bookstore.Models;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public List<Book> Books {get; set; } = new();
    public AuthorInfo? Info {get; set; }
}