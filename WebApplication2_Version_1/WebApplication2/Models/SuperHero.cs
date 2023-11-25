namespace WebApplication2.Models;

public class SuperHero
{
    public string Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string Place { get; set; } = string.Empty;

    private SuperHero()
    {
        
    }

    public static SuperHero Create(string name, string firstName, string lastName, string place)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name can't be empty ! ");
        
        if (string.IsNullOrWhiteSpace(firstName))
            throw new Exception("First name can't be empty !");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new Exception("Last name can't be empty !");

        if (string.IsNullOrWhiteSpace(place))
            throw new Exception("You should enter a place ! ");

        return new SuperHero
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            FirstName = firstName,
            LastName = lastName,
            Place = place
        };
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name can't be empty");

        name = name.Replace(" ", " ");
        Name = name;
    }

    public void SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new Exception("First name can't be empty");

        firstName = firstName.Replace("","");
        FirstName = firstName;
    }

    public void SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new Exception("Last name can't be empty");

        lastName = lastName.Replace(" ","");
        LastName = lastName;
    }

    public void SetPlace(string place)
    {
        if (string.IsNullOrWhiteSpace(place))
            throw new Exception("Place must exist ");

        place = place.Replace("", "");
        Place = place;
    }
    
}

