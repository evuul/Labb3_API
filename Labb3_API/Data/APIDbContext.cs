using Labb3_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3_API.Data;

public class APIDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Interest> Interests { get; set; }
    public DbSet<Link> Links { get; set; }

    public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Konfigurera många-till-många mellan Person och Interest
        modelBuilder.Entity<Person>()
            .HasMany(p => p.Interests)
            .WithMany(i => i.Persons)
            .UsingEntity<Dictionary<string, object>>(
                "PersonInterest",
                j => j.HasOne<Interest>().WithMany().HasForeignKey("InterestsId"),
                j => j.HasOne<Person>().WithMany().HasForeignKey("PersonsId"),
                j =>
                {
                    j.HasKey("PersonsId", "InterestsId");
                    j.HasData(
                        new { PersonsId = 1, InterestsId = 1 },
                        new { PersonsId = 1, InterestsId = 2 },
                        new { PersonsId = 2, InterestsId = 1 }
                    );
                });

        // Konfigurera relationer för Link
        modelBuilder.Entity<Link>()
            .HasOne(l => l.Person)
            .WithMany(p => p.Links)
            .HasForeignKey(l => l.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Link>()
            .HasOne(l => l.Interest)
            .WithMany(i => i.Links)
            .HasForeignKey(l => l.InterestId)
            .OnDelete(DeleteBehavior.Cascade);

        // ----------------------------------------
        // SEED-DATA START
        // ----------------------------------------

        // Seed personer
        modelBuilder.Entity<Person>().HasData(
            new Person { Id = 1, FirstName = "Alexander", LastName = "Ek", PhoneNumber = "070-1234567" },
            new Person { Id = 2, FirstName = "Karin", LastName = "Bengtsson", PhoneNumber = "073-7654321" }
        );

        // Seed intressen
        modelBuilder.Entity<Interest>().HasData(
            new Interest { Id = 1, Title = "Kodning", Description = "Lära mig allt om programmering" },
            new Interest { Id = 2, Title = "Natur", Description = "Vara ute i naturen" }
        );

        // Seed länkar kopplade till både personer och intressen
        modelBuilder.Entity<Link>().HasData(
            new Link { Id = 1, Url = "https://evodata.vercel.app/", PersonId = 1, InterestId = 1 },
            new Link { Id = 2, Url = "https://github.com/evuul", PersonId = 1, InterestId = 2 },
            new Link { Id = 3, Url = "https://feber.se", PersonId = 2, InterestId = 1 }
        );

        // ----------------------------------------
        // SEED-DATA SLUT
        // ----------------------------------------
    }
}