using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[ApiController]
[Route("[controller]")]

public class SuperHeroController : ControllerBase
{
    private static readonly List<SuperHero> _list = new()
    {
        SuperHero.Create("Spider-man", "Peter", "Parker", "New York City"),
        SuperHero.Create("Hulk", "Henry", "cacat", "Alabama")
    };

    [HttpGet(Name = "GetAllSuperheroes")]
    public ActionResult GetSuperheroes()
    {
        return Ok(_list);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult GetSingleHero(string id)
    {
        var superhero = _list.Find(super => super.Id == id);

        if (superhero == null)
            return NotFound($"SuperHero with id : {id} does not exist ! ");

        return Ok(superhero);

    }

    [HttpPost]

    public ActionResult AddHero([FromBody] SuperHeroRequest superHeroRequest)
    {

        SuperHero superhero = null;

        try
        {
            superhero = SuperHero.Create(superHeroRequest.Name, superHeroRequest.FirstName, superHeroRequest.LastName,
                superHeroRequest.Place);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _list.Add(superhero);
        return Ok(superHeroRequest);
    }

    [HttpPut("{Id}")]

    public ActionResult UpdateHero(string Id, [FromBody] SuperHeroRequest superHeroRequest)
    {
        var superhero = _list.Find(super => super.Id == Id);

        if (superhero == null)
            return NotFound($"Superhero with {Id} does not exist ! ");

        try
        {
            superhero.SetFirstName(superHeroRequest.FirstName);
            superhero.SetLastName(superHeroRequest.LastName);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(superhero);
    }

    [HttpDelete("{Id}")]
    public ActionResult DeleteHero(string Id)
    {
        var superhero = _list.Find(super => super.Id == Id);

        if (superhero == null) 
            return NotFound($"Superhero with {Id} does not exist ! ");
        _list.Remove(superhero);

        return Ok($"Superhero with {Id} was removed ! ");
    }
    
}