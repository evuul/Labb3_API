using Labb3_API.Models.DTO;

namespace Labb3_API.Services;

public interface IPersonService
{
    Task<IEnumerable<PersonDTO>> GetAllPersonsAsync();
    Task<PersonDTO?> GetPersonByIdAsync(int id);
    Task<IEnumerable<InterestDTO>> GetInterestsForPersonAsync(int personId);
    Task<IEnumerable<GetLinksForPersonDTO>> GetLinksForPersonAsync(int personId);
    Task<bool> UpdatePersonAsync(UpdatePersonDTO person);
    Task<bool> DeletePersonAsync(int id);
    Task AddInterestToPersonAsync(int personId, int interestId);
    Task AddLinkAsync(int personId, int interestId, CreateLinkDTO dto);
}