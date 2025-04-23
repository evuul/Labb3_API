namespace Labb3_API.Models;

public class Interest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<PersonInterest> PersonInterests { get; set; }
}