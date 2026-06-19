using backend.Data.Mappers;
using backend.Data.Requests;
using backend.Services;

namespace backend.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        // GET /users
        app.MapGet(
                "/users",
                async (ICvService cvService) =>
                {
                    var users = await cvService.GetAllUsersAsync();
                    var userDtos = users.Select(u => u.ToDto()).ToList();

                    return Results.Ok(userDtos);
                }
            )
            .WithName("GetAllUsers")
            .WithTags("Users");

        // GET /users/{id}
        // TODO: Oppgave 1: skriv et endepunkt for å hente ut riktig bruker

        app.MapGet(
                "/users/{id}",
                async (ICvService cvService, Guid id) =>
                {
                    var user = await cvService.GetUserByIdAsync(id);
                    if (user == null) return Results.Problem("No user found with given ID", statusCode: 404);
                    var userDto = user.ToDto();

                    return Results.Ok(userDto);
                }
            )
            .WithName("GetUserById")
            .WithTags("Users");



        // Retrieve all cvs that include any of the wanted skills
        app.MapPost(
                "/users/skills/",
                async (ICvService cvService, SkillRequest skills) =>
                {
                    var parsedSkills = skills.WantedSkills;
                    var usersWithWantedSkills = await cvService.GetUsersWithDesiredSkills(parsedSkills);
                    return Results.Ok(usersWithWantedSkills.Select(u => u.ToDto()).ToList());
                }
            )
            .WithName("GetUsersWithDesiredSkill")
            .WithTags("Users");
    }
}
