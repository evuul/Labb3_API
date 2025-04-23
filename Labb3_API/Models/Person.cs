namespace Labb3_API.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public List<PersonInterest> PersonInterests { get; set; }
}
