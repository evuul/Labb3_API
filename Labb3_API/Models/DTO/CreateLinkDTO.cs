namespace Labb3_API.Models.DTO;

public class CreateLinkDTO
{
    public string Url { get; set; } = string.Empty;
    public int PersonId { get; set; }
    public int InterestId { get; set; } 
}