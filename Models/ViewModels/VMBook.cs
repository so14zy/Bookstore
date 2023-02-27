namespace Bookstore.Models.ViewModels;

public class VMBook
{
    public string Title { get; set; }
    public string SecondName { get; set; }
    public string Genre { get; set; }
    public int Price { get; set; }
    public bool IsAvailable { get; set; }
    public string Language { get; set; }
    public int DateWritten { get; set; }
}