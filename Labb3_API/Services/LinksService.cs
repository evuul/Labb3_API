using Labb3_API.Data;
using Labb3_API.Models;
using Labb3_API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Labb3_API.Services;

public class LinksService : ILinksService
{
    private readonly APIDbContext _context;

    public LinksService(APIDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<ShowLinksDTO>> GetAllLinksAsync()
    {
        return await _context.Links
            .Select(link => new ShowLinksDTO
            {
                Id = link.Id,
                Url = link.Url,
                InterestId = link.InterestId
            })
            .ToListAsync();
    }
    

    public async Task<LinkDTO> CreateLinkAsync(CreateLinkDTO dto)
    {
        var link = new Link
        {
            PersonId = dto.PersonId,
            Url = dto.Url,
            InterestId = dto.InterestId
        };

        _context.Links.Add(link);
        await _context.SaveChangesAsync();

        return new LinkDTO
        {
            Id = link.Id,
            Url = link.Url,
            PersonId = link.PersonId,
            InterestId = link.InterestId
        };
    }
    
    public async Task<bool> DeleteLinkAsync(int id)
    {
        var link = await _context.Links.FindAsync(id);
        if (link == null)
            return false;

        _context.Links.Remove(link);
        await _context.SaveChangesAsync();

        return true;
    }
}