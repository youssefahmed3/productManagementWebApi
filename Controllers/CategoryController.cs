using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Dtos;
using ProductManagement.Helpers;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepsoitory _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepsoitory categoryRepsoitory)
        {
            _categoryRepository = categoryRepsoitory;
            _mapper = new Mapper(new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<CategoryToCreateDto, Category>();

           }));
        }

        [HttpGet("Categories")]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return Ok(_categoryRepository.GetAllCategories());
        }

        [HttpGet("Category/{categoryId}")]
        public ActionResult<Product> GetSingleCategory(int categoryId)
        {
            return Ok(_categoryRepository.GetSingleCategory(categoryId));
        }

        [HttpPost("Category")]
        public ActionResult AddCategory(CategoryToCreateDto categoryToCreate)
        {
            Category categoryDb = _mapper.Map<Category>(categoryToCreate);
            _categoryRepository.AddEntity<Category>(categoryDb);

            if (_categoryRepository.SaveChanges())
            {
                return Ok("Category Added Successfully");
            }
            throw new Exception("Error adding Category");
        }

        [HttpPut("Category")]
        public ActionResult EditCategory(CategoryToEditDto category)
        {
            Category? categoryDb = _categoryRepository.GetSingleCategoryWithoutProducts(category.Id);
            Console.WriteLine($"category id : {categoryDb.Id}");
            if (categoryDb != null)
            {
                categoryDb.Name = category.Name;
                
                if (_categoryRepository.SaveChanges())
                {
                    return Ok("Category Updated Successfully");
                }

                throw new Exception("Failed to Update Category");
            }

            throw new Exception("Failed to Get Category");
        }

        [HttpDelete("Category/{CategoryId}")]
        public ActionResult DeleteProduct(int CategoryId)
        {
            Category? categoryDb = _categoryRepository.GetSingleCategory(CategoryId);
             _categoryRepository.RemoveEntity<Category>(categoryDb);

            if (_categoryRepository.SaveChanges())
            {
                return Ok("Category Removed Successfully");
            }
            throw new Exception("Error Removing Category");
        }
        

    }
}
