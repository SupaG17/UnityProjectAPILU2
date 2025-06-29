using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProjectMap.WebApi.Entities
{
    public class Environment2D
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int MaxLength { get; set; }

        [Required]
        public int MaxHeight { get; set; }

        [Required]
        public string BackgroundColor { get; set; } = "#FFFFFF";

        // Foreign key for User
        [Required]
        public string UserId { get; set; }

        // Navigation property for User
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        public ICollection<GameObject> GameObjects { get; set; }

    }
}
