namespace Labb3_API.Models;

public class Interest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public ICollection<Person> Persons { get; set; } = new List<Person>();
    public ICollection<Link> Links { get; set; } = new List<Link>();
}