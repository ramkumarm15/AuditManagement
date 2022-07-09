using System.ComponentModel.DataAnnotations;

namespace AuthorizationMicroservice.Models;

public class UserData
{
    [MaxLength(50)]
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [MaxLength(50)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
