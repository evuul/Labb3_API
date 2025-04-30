namespace Labb3_API.Models.DTO;

public class UpdatePersonDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Mobilnummer { get; set; }
    public List<int>? InterestIds { get; set; }
    public List<CreateLinkDTO>? Links { get; set; } = new(); // Använd samma typ som vid POST

}