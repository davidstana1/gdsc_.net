using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;

public record SuperHeroRequest(string Name, string FirstName, string LastName, string Place);

public class SuperHeroRequest1
{
    public string Name { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Place { get; init; }
}
