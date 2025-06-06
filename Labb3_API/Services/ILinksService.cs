using Labb3_API.Models.DTO;

namespace Labb3_API.Services;

public interface ILinksService
{
    Task<IEnumerable<ShowLinksDTO>> GetAllLinksAsync();
    Task<LinkDTO> CreateLinkAsync(CreateLinkDTO dto);
    Task<bool> DeleteLinkAsync(int id);
}