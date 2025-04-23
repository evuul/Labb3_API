namespace Labb3_API.Models.DTO;

public class LinkDTO
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public int PersonId { get; set; }
    public int InterestId { get; set; }
}