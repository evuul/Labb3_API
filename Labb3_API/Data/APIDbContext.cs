using Labb3_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3_API.Data;

public class APIDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Interest> Interests { get; set; }
    public DbSet<Link> Links { get; set; }
    public DbSet<PersonInterest> PersonInterests { get; set; }
    
    public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
    {
        
    }
}