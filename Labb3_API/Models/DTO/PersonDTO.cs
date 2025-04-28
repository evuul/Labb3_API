using System.Text.Json.Serialization;
using Microsoft.Build.Framework;

namespace Labb3_API.Models.DTO;

public class PersonDTO
{
    public string Name { get; set; } = string.Empty;
    public string? Mobilnummer { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<InterestDTO>? Interests { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<LinkDTO>? Links { get; set; }
}