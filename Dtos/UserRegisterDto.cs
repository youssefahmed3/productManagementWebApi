using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Dtos;

public class UserRegisterDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }


    public UserRegisterDto()
    {
        FirstName ??= "";
        LastName ??= "";
        Email ??= "";
        Password ??= "";
    }
}
