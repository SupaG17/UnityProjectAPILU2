using System.ComponentModel.DataAnnotations;

namespace ProjectMap.WebApi.Requests
{
    public class CreateEnvironment2DDto
    {
        [Required]
        [StringLength(25, ErrorMessage = "Name must be between 1 and 25 characters.", MinimumLength = 1)]
        public string Name { get; set; }

        [Range(1, 1000, ErrorMessage = "MaxLength must be between 1 and 1000.")]
        public int MaxLength { get; set; }

        [Range(1, 1000, ErrorMessage = "MaxHeight must be between 1 and 1000.")]
        public int MaxHeight { get; set; }
    }
}
