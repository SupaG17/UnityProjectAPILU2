using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMap.WebApi.DataAccess;
using ProjectMap.WebApi.Entities;
using ProjectMap.WebApi.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectMap.WebApi.Controllers;

[Route("api/environments")]
[Authorize]
[ApiController]
public class EnvironmentController : ControllerBase
{
    private readonly GameContext _gameContext;
    public EnvironmentController(GameContext gameContext)
    {
        this._gameContext = gameContext;
    }
    // GET: api/<WorldController>
    [HttpGet]
    public ActionResult<IEnumerable<Environment2D>> Get()
    {
        // get the user id from the token and filter the environments by user id
        var userIdentifier = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var userDbId = this._gameContext.Users.FirstOrDefault(u => u.UserName == userIdentifier).Id;
        var environments = this._gameContext.Environments
            .Include(e => e.GameObjects)
            .Where(e => e.UserId == userDbId)
            .ToArray();
        return environments;
    }

    // GET api/<WorldController>/5
    [HttpGet("{id}")]
    public ActionResult<Environment2D> Get(int id)
    {
        var environment = this._gameContext.Environments
            .Include(e => e.GameObjects)
            .FirstOrDefault(e => e.Id == id);

        if (environment == null) { 
            return NotFound();
        }
        return environment;
    }

    // POST api/<WorldController>
    [HttpPost]
    public ActionResult<Environment2D> Post([FromBody] CreateEnvironment2DDto environmentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdentifier = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        var userDbId = this._gameContext.Users.FirstOrDefault(u => u.UserName == userIdentifier).Id;

        var environment = new Environment2D
        {
            Name = environmentDto.Name,
            MaxLength = environmentDto.MaxLength,
            MaxHeight = environmentDto.MaxHeight,
            UserId = userDbId
        };

        // check if the environment already exists with the same name if so return 409
        var existingEnvironment = this._gameContext.Environments.FirstOrDefault(e => e.Name == environment.Name);
        if (existingEnvironment != null)
        {
            return Conflict(new { message = "Environment with the same name already exists." });
        }

        // count the number of environments of the user
        var environmentCount = this._gameContext.Environments.Count(e => e.UserId == userDbId);

        // if the number of environments is greater than 5 return 409
        if (environmentCount >= 5)
        {
            return Conflict(new { message = "You can only create 5 environments." });
        }


        this._gameContext.Environments.Add(environment);
        this._gameContext.SaveChanges();

        return CreatedAtAction(nameof(Get), new { id = environment.Id }, environment);
    }


    // PUT api/<WorldController>/5
    [HttpPut("{id}")]
    public ActionResult<Environment2D> Put(int id, [FromBody] UpdateEnvironment2DDto environmentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var environment = this._gameContext.Environments.Find(id);

        if (environment == null)
        {
            return NotFound();
        }

        environment.Name = environmentDto.Name;
        environment.MaxLength = environmentDto.MaxLength;
        environment.MaxHeight = environmentDto.MaxHeight;

        this._gameContext.Environments.Update(environment);
        this._gameContext.SaveChanges();

        return NoContent();
    }



    // DELETE api/<WorldController>/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var environment = this._gameContext.Environments.Find(id);
        if (environment == null)
        {
            return NotFound();
        }
        this._gameContext.Environments.Remove(environment);
        this._gameContext.SaveChanges();
        return NoContent();
    }
}

