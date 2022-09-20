using System.ComponentModel.DataAnnotations;

namespace GameHub.Common.Models.RequestModels
{
    public class UserRegistrationRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
