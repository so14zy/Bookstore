using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookstore.Models;

namespace Bookstore.DAL.Configuration;
public class RoleConfiguration : DataReader<Role>, IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasMany(a => a.Users)
                .WithOne(u => u.Role);

        List<Role>? roles = readData("DAL/Configuration/Data/Role data.json");
        builder.HasData(roles);
    }
}