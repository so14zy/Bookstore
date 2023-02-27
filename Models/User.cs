using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models;

public class User
{
    public int Id { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}