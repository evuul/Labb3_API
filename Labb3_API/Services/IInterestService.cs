using Labb3_API.Models.DTO;

namespace Labb3_API.Services;

public interface IInterestService
{
    Task<IEnumerable<ShowInterestDTO>> GetAllInterestsAsync();
    Task<CreatedInterestDTO> CreateInterestAsync(CreateInterestDTO dto);
}