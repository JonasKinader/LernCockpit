using System.ComponentModel.DataAnnotations;

namespace LernCockpit.Components.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(5)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        [MinLength(8)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;
        
        public bool IsAdmin { get; set; } 
        
        public bool IsLogedIn { get; set; }

        public bool IsAusbilder { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}