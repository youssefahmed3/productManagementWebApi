using Microsoft.AspNetCore.Identity;
using ProductManagement.Dtos;

namespace ProductManagement.Models {
    public class Category {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; } // navigation property

        public Category () {
            Name ??= "";
            Products = [];
        }
    }
}