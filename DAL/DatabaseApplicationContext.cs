using Microsoft.EntityFrameworkCore;
using Bookstore.Models;
using Bookstore.DAL.Configuration;

namespace Bookstore.DAL;

public class DatabaseApplicationContext : DbContext
{
    readonly StreamWriter logStream = new StreamWriter("mylog.txt", true);

    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Language> Languages { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<AuthorInfo> AuthorInfos { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<BookInfo> BookInfos { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
   
    public DatabaseApplicationContext(DbContextOptions<DatabaseApplicationContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(logStream.WriteLine);
    }
    

    public override void Dispose()
    {
        base.Dispose();
        logStream.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await logStream.DisposeAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new LanguageConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorInfoConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new BookInfoConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}