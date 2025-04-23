using System.ComponentModel.DataAnnotations;

namespace Labb3_API.Models.DTO;

public class CreatePersonDTO
{
    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}