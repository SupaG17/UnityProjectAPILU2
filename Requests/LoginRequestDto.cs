using System.ComponentModel.DataAnnotations;

namespace ProjectMap.WebApi.Requests
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
