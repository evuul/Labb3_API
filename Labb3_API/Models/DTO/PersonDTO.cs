using Microsoft.Build.Framework;

namespace Labb3_API.Models.DTO;

public class PersonDTO
{
    [Required]
    public string FirstName { get; set; } = String.Empty;
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    
    public List<InterestDTO>? Interests { get; set; } = new List<InterestDTO>();
}