using System.ComponentModel.DataAnnotations;

namespace ProjectMap.WebApi.Requests
{
    public class UpdateEnvironment2DDto
    {
        [Required]
        public string Name { get; set; }

        [Range(1, 1000, ErrorMessage = "MaxHeight must be between 1 and 1000.")]
        public int MaxLength { get; set; }

        [Range(1, 1000, ErrorMessage = "MaxHeight must be between 1 and 1000.")]
        public int MaxHeight { get; set; }
    }
}
