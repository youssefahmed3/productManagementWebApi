using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApi.Models;
using ProductManagement.Data;
using ProductManagement.Dtos;
using ProductManagement.Helpers;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly DataContextEF _entityFramework;

        public ProductRepository(UserManager<User> userManager, IConfiguration config, DataContextEF entityFramwork)
        {
            _userManager = userManager;
            _config = config;
            _entityFramework = entityFramwork; // dependency injection 
        }

        public void AddEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null)
            {
                _entityFramework.Add(entityToAdd);
            }
        }

        public void RemoveEntity<T>(T entityToRemove)
        {
            if (entityToRemove != null)
            {
                _entityFramework.Remove(entityToRemove);
            }
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var products = _entityFramework.Products?
                .Include(p => p.Category) // Eagerly load the Category
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,  // You can explicitly select the category name here
                })
                .ToList();

            if (products != null)
            {
                return products;
            }
            else
            {
                throw new Exception("No products found in the database.");
            }
        }


        public Product GetSingleProduct(int productId)
        {
            Product? product = _entityFramework.Products?.Where(product => product.Id == productId).FirstOrDefault<Product>();

            if (product != null)
            {
                return product;
            }
            throw new Exception($"No note found with ID: {productId}");
        }


    }
}