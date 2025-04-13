using System.ComponentModel.DataAnnotations;
using ProjectMap.WebApi.Requests;

public class UpdateGameObjectsDto
{
    [Required]
    public int Environment2DId { get; set; }

    [Required]
    public List<CreateGameObjectDto> GameObjects { get; set; }
}
