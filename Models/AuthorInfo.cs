namespace Bookstore.Models;

public class AuthorInfo
{
    public int AuthorId { get; set; }
    public int? YearOfBirth { get; set; }
    public int? YearOfDeath { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;
    public Author Author { get; set; } = null!;
}