using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Bookstore.Models;

namespace Bookstore.DAL.Configuration;
public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
         builder.HasMany(a => a.Books)
                .WithOne(b => b.Author);

        string str = File.ReadAllText("DAL/Configuration/Data/Author data.json");
        List<Author>? authors = JsonConvert.DeserializeObject<List<Author>>(str);
        builder.HasData(authors);
    }
}