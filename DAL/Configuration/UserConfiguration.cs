using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookstore.Models;

namespace Bookstore.DAL.Configuration;
public class UserConfiguration : DataReader<User>, IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        List<User>? users = readData("DAL/Configuration/Data/User data.json");
        builder.HasData(users);
    }
}