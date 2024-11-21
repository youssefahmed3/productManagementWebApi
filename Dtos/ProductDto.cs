using Microsoft.AspNetCore.Identity;

namespace ProductManagement.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; } // Foreign Key For the Category Table
        public string CategoryName { get; set; } // Navigation Property


        public ProductDto()
        {
            Name ??= "";
            Description ??= "";

        }
    }
}