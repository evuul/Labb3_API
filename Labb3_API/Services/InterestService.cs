using Labb3_API.Data;
using Labb3_API.Models;
using Labb3_API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Labb3_API.Services;

public class InterestService : IInterestService
{
    private readonly APIDbContext _context;

    public InterestService(APIDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InterestDTO>> GetAllInterestsAsync()
    {
        var interests = await _context.Interests
            .Select(i => new InterestDTO
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description
            })
            .ToListAsync();

        return interests;
    }
    
    public async Task<InterestDTO> CreateInterestAsync(CreateInterestDTO dto)
    {
        var interest = new Interest
        {
            Title = dto.Title,
            Description = dto.Description
        };

        _context.Interests.Add(interest);
        await _context.SaveChangesAsync();

        return new InterestDTO
        {
            Id = interest.Id,
            Title = interest.Title,
            Description = interest.Description,
            Links = new List<string>()
        };
    }
}
    