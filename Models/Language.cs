namespace Bookstore.Models;

public class Language
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<BookInfo> BookInfos {get; set; } = new();
}