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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepsoitory _categoryRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, ICategoryRepsoitory categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = new Mapper(new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<ProductToCreateDto, Product>();

           }));
        }

        [HttpGet("Products")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_productRepository.GetAllProducts());
        }

        [HttpGet("Product/{productId}")]
        public ActionResult<Product> GetSingleProduct(int productId)
        {
            return Ok(_productRepository.GetSingleProduct(productId));
        }

        [HttpPost("Product")]
        public ActionResult AddProduct(ProductToCreateDto productAdd)
        {
            // Map DTO to Product
            Product productDb = _mapper.Map<Product>(productAdd);

            // Fetch the category from the database
            Category categoryDb = _categoryRepository.GetSingleCategory(productAdd.CategoryId);
            if (categoryDb == null)
            {
                throw new Exception("Category not found");
            }

            // Link the product to the existing category
            productDb.CategoryId = categoryDb.Id;

            // Add the product to the repository
            _productRepository.AddEntity<Product>(productDb);

            // Save changes
            if (_productRepository.SaveChanges())
            {
                return Ok($"Product '{productAdd.Name}' added to Category '{categoryDb.Name}' successfully.");
            }

            throw new Exception("Error adding product");
        }

        [HttpPut("Product")]
        public ActionResult EditProduct(ProductToEditDto product)
        {
            Product? productDb = _productRepository.GetSingleProduct(product.Id);

            if (productDb != null)
            {
                productDb.Name = product.Name;
                productDb.Description = product.Description;
                productDb.Price = product.Price;
                // productDb.CategoryId = product.CategoryId;
                Category updatedCategory = _categoryRepository.GetSingleCategory(product.CategoryId);
                
                if(updatedCategory != null) {
                    productDb.CategoryId = updatedCategory.Id;
                    productDb.Category = updatedCategory;
                }
                else {
                    throw new Exception("You are trying to add a product to a Category That does't Exist");
                }


                if (_productRepository.SaveChanges())
                {
                    return Ok();
                }

                throw new Exception("Failed to Update Product");
            }

            throw new Exception("Failed to Get Product");
        }

        [HttpDelete("Product/{productId}")]
        public ActionResult DeleteProduct(int productId)
        {
            Product? productDb = _productRepository.GetSingleProduct(productId);
            _productRepository.RemoveEntity<Product>(productDb);

            if (_productRepository.SaveChanges())
            {
                return Ok("product Removed Successfully");
            }
            throw new Exception("Error Removing Product");
        }

        /* [HttpGet("AddProductToCategory")]
        public ActionResult AddProductToCategory(AddProductToCategoryDto addProductToCategory)
        {
            Product? product = _productRepository.GetSingleProduct(addProductToCategory.ProductId);
            if (product != null)
            {
                Category? category = _categoryRepository.GetSingleCategory(addProductToCategory.CategoryId);

                if (category != null)
                {
                    category.Products.Add(product);
                    if (_categoryRepository.SaveChanges())
                    {
                        return Ok("Product Added to Category Successfully");
                    }
                }
                throw new Exception("Category Not Found");
            }

            throw new Exception("Product Not Found");
        } */
    }
}