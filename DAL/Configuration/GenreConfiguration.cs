using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookstore.Models;

namespace Bookstore.DAL.Configuration;
public class GenreConfiguration : DataReader<Genre>, IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasMany(a => a.Books)
                .WithOne(b => b.Genre);

        List<Genre>? genres = readData("DAL/Configuration/Data/Genre data.json");
        builder.HasData(genres);
    }
}