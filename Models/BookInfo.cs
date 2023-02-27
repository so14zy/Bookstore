namespace Bookstore.Models;

public class BookInfo
{
    public int BookId { get; set; }
    public Book Book { get; set; } =null!;
    public int Price { get; set; }
    public bool IsAvailable { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; } =null!;
    public int DateWritten { get; set; }
}