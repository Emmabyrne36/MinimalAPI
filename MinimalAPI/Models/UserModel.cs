using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public record UserModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
