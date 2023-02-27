using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Bookstore.Models;

namespace Bookstore.DAL.Configuration;

public class AuthorInfoConfiguration : DataReader<AuthorInfo>, IEntityTypeConfiguration<AuthorInfo>
{
    public void Configure(EntityTypeBuilder<AuthorInfo> builder)
    {
        builder.HasKey(u => u.AuthorId);
        builder.HasOne(u => u.Author)
               .WithOne(b => b.Info)
               .HasForeignKey<AuthorInfo>(b => b.AuthorId);

        string str = File.ReadAllText("DAL/Configuration/Data/AuthorInfo data.json");
        List<AuthorInfo>? authors = JsonConvert.DeserializeObject<List<AuthorInfo>>(str);
        builder.HasData(authors);
    }
}