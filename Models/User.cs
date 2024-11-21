using Microsoft.AspNetCore.Identity;

namespace ProductManagement.Models;
public class User : IdentityUser {

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    
    public List<Product> Products { get; set; } // navigation Property
    public List<string> Roles { get; set; } = new List<string>();

    public User() {
        FirstName ??= "";
        LastName ??= "";
        Gender ??= "";
        Products = [];
    }
}