using Microsoft.EntityFrameworkCore;
using Bookstore.Models;

namespace Bookstore.DAL.Repositories;

public class UserRepository : IUserRepository
{
    DatabaseApplicationContext _db;

    public UserRepository(DatabaseApplicationContext db)
    {
        _db = db;
    }

    public async void add(User user)
    {
        User? tempUser = await getById(user.Id);
        if (tempUser == null)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }
    }

    public async Task<User> getById(int id)
    {
        User? user = await _db.Users
            .Where(u => u.Id == id)
            .Include(u => u.Role)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<User> getByEmail(string email)
    {
        User? user = await _db.Users
            .Where(u => u.Email == email)
            .Include(u => u.Role)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<List<User>> getAll()
    {
        List<User> users = await _db.Users
            .OrderBy(u => u.Email)
            .Include(u => u.Role)
            .ToListAsync();

        return users;
    }

     public async void update(User user)
    {
        User? tempUser = await getById(user.Id);
        if (tempUser == null)
            _db.Users.Add(user);
        else
        {
            tempUser.Email = user.Email;
            tempUser.Password = user.Password;
            tempUser.RoleId = user.RoleId;
        }

        _db.SaveChanges();
    }

    public async void delete(int id)
    {
        User? user = await getById(id);
        if (user != null)
        {
            _db.Users.Remove(user);
            _db.SaveChanges();
        }
    }
}