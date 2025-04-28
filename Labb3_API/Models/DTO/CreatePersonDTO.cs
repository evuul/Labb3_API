using System.ComponentModel.DataAnnotations;

namespace Labb3_API.Models.DTO;

public class CreatePersonDTO
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;

    public List<int> InterestIds { get; set; } = new();
    public List<CreateLinkDTO> Links { get; set; } = new();

}