using Microsoft.AspNetCore.Identity;

namespace ProductManagement.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; } // Foreign Key For the Category Table
        public virtual Category Category { get; set; } // Navigation Property


        public Product()
        {
            Name ??= "";
            Description ??= "";

        }
    }
}