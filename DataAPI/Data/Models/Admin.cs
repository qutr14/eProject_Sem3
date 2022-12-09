using System.ComponentModel.DataAnnotations;

namespace DataAPI.Data.Models
{
    public class Admin
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Role { get; set; }
        public string Details { get; set; }
    }
    public class AdminLogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
    public class PasswordHashing
    {
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    } 
}
