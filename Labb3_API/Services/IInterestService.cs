using Labb3_API.Models.DTO;

namespace Labb3_API.Services;

public interface IInterestService
{
    Task<IEnumerable<InterestDTO>> GetAllInterestsAsync();
    Task<InterestDTO> CreateInterestAsync(CreateInterestDTO dto);
}