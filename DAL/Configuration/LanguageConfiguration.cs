using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookstore.Models;

namespace Bookstore.DAL.Configuration;
public class LanguageConfiguration : DataReader<Language>, IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        
        builder.HasMany(a => a.BookInfos)
                .WithOne(b => b.Language);

        List<Language>? languages = readData("DAL/Configuration/Data/Language data.json");
        builder.HasData(languages);
    }
}