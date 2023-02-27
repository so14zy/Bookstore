namespace Bookstore.Models;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<AuthorInfo> AuthorInfos {get; set; } = new();
}