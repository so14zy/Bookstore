using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Bookstore.Models;

namespace Bookstore.DAL.Configuration;
public class BookConfiguration : DataReader<Book>, IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        string str = File.ReadAllText("DAL/Configuration/Data/Book data.json");
        List<Book>? books = JsonConvert.DeserializeObject<List<Book>>(str);
        builder.HasData(books);
    }
}