using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthorizationMicroservice.Models;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(50)]
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [MaxLength(50)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
