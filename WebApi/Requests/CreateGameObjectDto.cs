using System.ComponentModel.DataAnnotations;

namespace ProjectMap.WebApi.Requests
{
    public class CreateGameObjectDto
    {
        
        [Range(float.MinValue, float.MaxValue)]
        public float Xposition { get; set; }

        [Range(float.MinValue, float.MaxValue)]
        public float Yposition { get; set; }

        [Required]
        public string PrefabId { get; set; }

        [Range(1, int.MaxValue)]
        public int EnvironmentId { get; set; }

    }
}

