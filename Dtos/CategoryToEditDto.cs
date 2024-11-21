using Microsoft.AspNetCore.Identity;

namespace ProductManagement.Dtos {
    public class CategoryToEditDto {
        public int Id { get; set; }
        public string Name { get; set; }


        public CategoryToEditDto () {
            Name ??= "";
        }
    }
}