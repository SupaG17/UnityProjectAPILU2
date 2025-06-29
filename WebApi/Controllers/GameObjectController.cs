using Microsoft.AspNetCore.Mvc;
using ProjectMap.WebApi.DataAccess;
using ProjectMap.WebApi.Entities;

namespace ProjectMap.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameObjectController : ControllerBase
{
    private readonly GameContext _gameContext;
    public GameObjectController(GameContext gameContext)
    {
        this._gameContext = gameContext;
    }


    [HttpPost("update")]
    public ActionResult UpdateGameObjects([FromBody] UpdateGameObjectsDto updateGameObjectsDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var environment = this._gameContext.Environments.Find(updateGameObjectsDto.Environment2DId);
        if (environment == null)
        {
            return NotFound(new { message = "Environment not found." });
        }

        // Remove existing GameObjects for the environment
        var existingGameObjects = this._gameContext.GameObjects.Where(go => go.Environment2DId == updateGameObjectsDto.Environment2DId);
        this._gameContext.GameObjects.RemoveRange(existingGameObjects);

        // Add new GameObjects
        foreach (var gameObjectDto in updateGameObjectsDto.GameObjects)
        {
            var gameObject = new GameObject
            {
                Xposition = gameObjectDto.Xposition,
                Yposition = gameObjectDto.Yposition,
                PrefabId = gameObjectDto.PrefabId,
                Environment2DId = updateGameObjectsDto.Environment2DId
            };
            this._gameContext.GameObjects.Add(gameObject);
        }

        this._gameContext.SaveChanges();

        return NoContent();
    }
}
