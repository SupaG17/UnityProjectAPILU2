using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectMap.WebApi.Entities
{
    public class GameObject
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public float Xposition { get; set; }
        
        [Required]
        public float Yposition { get; set; }

        [Required]
        public string PrefabId { get; set; }

        [Required]
        public int Environment2DId { get; set; }
        // Navigation property for Environment2D
        [ForeignKey("Environment2DId")]
        [JsonIgnore]
        public Environment2D Environment2D { get; set; }



    }
}
