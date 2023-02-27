using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookstore.Models;

namespace Bookstore.DAL.Configuration;
public class CountryConfiguration : DataReader<Country>, IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasMany(u => u.AuthorInfos)
                .WithOne(u => u.Country);

        List<Country>? countries = readData("DAL/Configuration/Data/Country data.json");
        builder.HasData(countries);
    }
}