using System.ComponentModel.DataAnnotations;

namespace UIUXLayer.Models
{
    public class UserLoginClass
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
