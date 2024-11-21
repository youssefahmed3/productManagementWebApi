using Microsoft.AspNetCore.Identity;

namespace ProductManagement.Dtos {
    public class CategoryToCreateDto {
        public string Name { get; set; }


        public CategoryToCreateDto () {
            Name ??= "";
        }
    }
}