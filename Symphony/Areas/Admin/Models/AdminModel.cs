using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Symphony.Areas.Admin.Models
{
    public class AdminModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Role { get; set; }
        public string Details { get; set; }
    }
    /// <summary>
    /// To login
    /// </summary>
    public class AdminLoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
    /// <summary>
    /// To get token
    /// </summary>
    public class AdminResultModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
    /// <summary>
    /// Recieve result when login include token
    /// </summary>
    public class AdminApiResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public AdminResultModel Data { get; set; }
    }
}
