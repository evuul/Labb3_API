using Labb3_API.Data;
using Labb3_API.Models;
using Labb3_API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Labb3_API.Services;

public class PersonService : IPersonService
{
    private readonly APIDbContext _context;

    public PersonService(APIDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<PersonDTO>> GetAllPersonsAsync()
    {
        return await _context.Persons
            .Select(p => new PersonDTO
            {
                Id = p.Id,
                Name = p.FirstName + " " + p.LastName,
                Mobilnummer = p.PhoneNumber,
                Interests = p.Interests.Select(i => new InterestDTO
                {
                    Id = i.Id,
                    Title = i.Title,
                    Description = i.Description,
                    Links = _context.Links
                        .Where(l => l.PersonId == p.Id && l.InterestId == i.Id)
                        .Select(l => l.Url!)
                        .ToList()
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<PersonDTO?> GetPersonByIdAsync(int id)
    {
        return await _context.Persons
            .Where(p => p.Id == id)
            .Select(p => new PersonDTO
            {
                Name = p.FirstName + " " + p.LastName,
                Mobilnummer = p.PhoneNumber,
                Interests = p.Interests.Select(i => new InterestDTO
                {
                    Title = i.Title,
                    Description = i.Description,
                    Links = _context.Links
                        .Where(l => l.PersonId == p.Id && l.InterestId == i.Id)
                        .Select(l => l.Url!)
                        .ToList()
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<InterestDTO>> GetInterestsForPersonAsync(int personId)
    {
        return await _context.Interests
            .Where(i => i.Persons.Any(p => p.Id == personId))
            .Select(i => new InterestDTO
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Links = _context.Links
                    .Where(l => l.PersonId == personId && l.InterestId == i.Id)
                    .Select(l => l.Url!)
                    .ToList()
            }).ToListAsync();
    }

    public async Task<IEnumerable<LinkDTO>> GetLinksForPersonAsync(int personId)
    {
        return await _context.Links
            .Where(l => l.PersonId == personId)
            .Select(l => new LinkDTO
            {
                Id = l.Id,
                Url = l.Url,
                PersonId = l.PersonId,
                InterestId = l.InterestId
            }).ToListAsync();
    }

    public async Task<bool> UpdatePersonAsync(UpdatePersonDTO dto)
    {
        var person = await _context.Persons
            .Include(p => p.Interests)
            .Include(p => p.Links)
            .FirstOrDefaultAsync(p => p.Id == dto.Id);

        if (person == null)
            return false;

        // Uppdatera grundläggande info
        person.FirstName = dto.FirstName;
        person.LastName = dto.LastName;
        person.PhoneNumber = dto.Mobilnummer ?? person.PhoneNumber; // Om Mobilnummer är null, behåll det gamla värdet

        // Uppdatera intressen
        if (dto.InterestIds != null)
        {
            var currentInterestIds = person.Interests.Select(i => i.Id).ToList();
            var interestsToAddIds = dto.InterestIds.Except(currentInterestIds).ToList();
            var interestsToRemove = person.Interests.Where(i => !dto.InterestIds.Contains(i.Id)).ToList();

            var interestsToAdd = await _context.Interests
                .Where(i => interestsToAddIds.Contains(i.Id))
                .ToListAsync();

            foreach (var interest in interestsToRemove)
                person.Interests.Remove(interest);

            foreach (var interest in interestsToAdd)
                person.Interests.Add(interest);
        }

        // Uppdatera länkar
        if (dto.Links != null)
        {
            var existingLinks = person.Links.ToList();

            var incomingUrls = dto.Links.Select(l => l.Url).ToList();
            var linksToRemove = existingLinks
                .Where(l => !incomingUrls.Contains(l.Url))
                .ToList();

            _context.Links.RemoveRange(linksToRemove);

            var newLinks = dto.Links
                .Where(l => !existingLinks.Any(el => el.Url == l.Url))
                .Select(l => new Link
                {
                    Url = l.Url,
                    InterestId = l.InterestId,
                    PersonId = dto.Id
                }).ToList();

            await _context.Links.AddRangeAsync(newLinks);
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public Task<bool> DeletePersonAsync(int id)
    {
        var person = _context.Persons.Find(id);
        if (person == null) return Task.FromResult(false);

        _context.Persons.Remove(person);
        return Task.FromResult(_context.SaveChanges() > 0);
    }


    public async Task AddInterestToPersonAsync(int personId, int interestId)
    {
        var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == personId);
        var interest = await _context.Interests.FindAsync(interestId);

        if (person == null || interest == null)
            return;

        if (!person.Interests.Any(i => i.Id == interestId))
        {
            person.Interests.Add(interest);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddLinkAsync(int personId, int interestId, CreateLinkDTO dto)
    {
        var exists = await _context.Persons.AnyAsync(p => p.Id == personId) &&
                     await _context.Interests.AnyAsync(i => i.Id == interestId);

        if (!exists) return;

        var link = new Link
        {
            Url = dto.Url,
            PersonId = personId,
            InterestId = interestId
        };

        _context.Links.Add(link);
        await _context.SaveChangesAsync();
    }
}