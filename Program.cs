using Microsoft.EntityFrameworkCore;
using Bookstore.DAL.Repositories;
using Bookstore.DAL;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseApplicationContext>(
    options => options.UseSqlite(connectionString)
);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<LanguageRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(option =>
        {
            option.LoginPath = "/Access/Login";
            option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            option.AccessDeniedPath = "/Access/Block";
        });

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();