using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Dtos;

public class ProductToCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; } // Foreign Key For the Category Table
    

    public ProductToCreateDto()
    {
        Name ??= "";
        Description ??= "";
    }
}
