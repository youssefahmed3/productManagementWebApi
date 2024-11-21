using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Dtos;

public class UserLoginDto
{
    
    
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    

    public UserLoginDto()
    {
        Password ??= "";
        Email ??= "";
    }
}
