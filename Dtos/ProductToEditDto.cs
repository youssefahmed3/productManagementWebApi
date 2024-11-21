using Microsoft.AspNetCore.Identity;

namespace ProductManagement.Dtos
{
    public class ProductToEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; } // Foreign Key For the Category Table


        public ProductToEditDto()
        {
            Name ??= "";
            Description ??= "";

        }
    }
}