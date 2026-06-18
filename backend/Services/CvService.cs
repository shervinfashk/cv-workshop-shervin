using backend.Data;
using backend.Data.Mappers;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class CvService(AppDbContext context) : ICvService
{
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await context.Users.OrderBy(u => u.Name).ToListAsync();
    }

    // TODO: Oppgave 1

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<Experience>> GetAllExperiencesAsync()
    {
        // TODO: Oppgave 2
        return await context.Experiences.OrderBy(u => u.Title).ToListAsync();
    }

    public async Task<Experience?> GetExperienceByIdAsync(Guid id)
    {
        // TODO: Oppgave 2

        return await context.Experiences.FindAsync(id);
    }

    public async Task<IEnumerable<Experience>> GetExperiencesByTypeAsync(string type)
    {
        return await context.Experiences.Where(u => u.Type == type).ToListAsync();

    }

    public async Task<IEnumerable<User>> GetUsersWithDesiredSkills(IEnumerable<string> desiredTechnologies)
    {
        var users = await GetAllUsersAsync();

        return users.Where(

            user => UserMapper.ParseUserSkills(user.Skills).Any(skill => desiredTechnologies.Contains(skill.Technology))


        ).ToList();










    }





    // TODO: Oppgave 4 ny metode (husk å legge den til i interfacet)
}
