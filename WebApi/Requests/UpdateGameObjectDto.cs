using System.ComponentModel.DataAnnotations;

namespace ProjectMap.WebApi.Requests
{
    public class UpdateGameObjectDto
    {
        [Required]
        public float Xposition { get; set; }

        [Required]
        public float Yposition { get; set; }

        [Required]
        public float Zposition { get; set; }

        [Required]
        public string PrefabId { get; set; }
    }
}
    

