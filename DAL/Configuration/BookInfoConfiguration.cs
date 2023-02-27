using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Bookstore.Models;

namespace Bookstore.DAL.Configuration;

public class BookInfoConfiguration : DataReader<BookInfo>, IEntityTypeConfiguration<BookInfo>
{
    public void Configure(EntityTypeBuilder<BookInfo> builder)
    {
        builder.HasKey(u => u.BookId);

        builder.HasOne(u => u.Book)
               .WithOne(b => b.Info)
               .HasForeignKey<BookInfo>(b => b.BookId);

        string str = File.ReadAllText("DAL/Configuration/Data/BookInfo data.json");
        List<BookInfo>? bookInfos = JsonConvert.DeserializeObject<List<BookInfo>>(str);
        builder.HasData(bookInfos);
    }
}