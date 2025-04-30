namespace Labb3_API.Models.DTO;

public class InterestDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public List<string> Links { get; set; } = new();
}