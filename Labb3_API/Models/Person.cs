using System.ComponentModel.DataAnnotations;

namespace Labb3_API.Models;

public class Person
{
    public int Id { get; set; }
    [Required] public string FirstName { get; set; } = String.Empty;
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }

    public ICollection<Interest> Interests { get; set; } = new List<Interest>();
    public ICollection<Link> Links { get; set; } = new List<Link>();
}
