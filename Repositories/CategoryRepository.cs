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
    public class CategoryRepsoitory : ICategoryRepsoitory
    {

        private readonly DataContextEF _entityFramework;

        public CategoryRepsoitory(DataContextEF entityFramwork)
        {
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

        public IEnumerable<Category> GetAllCategories()
        {
            IEnumerable<Category>? categories = _entityFramework.Categories?
                    .Include(c => c.Products) // Include products for mapping
                    .Select(c => new Category
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Products = c.Products.Select(p => new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            CategoryId = p.CategoryId
                        }).ToList()
                    })
                    .ToList();

            if (categories != null)
            {
                return categories;
            }
            else
            {
                throw new Exception("No categories found in the database.");
            }
        }

        public Category GetSingleCategory(int categoryId)
        {
            var category = _entityFramework.Categories?
                .Include(c => c.Products) // Include related data
                .FirstOrDefault(c => c.Id == categoryId);

            if (category != null)
            {
                return category;
            }

            throw new Exception($"No category found with ID: {categoryId}");
        }

        public Category GetSingleCategoryWithoutProducts(int categoryId)
        {
            var category = _entityFramework.Categories?
                .FirstOrDefault(c => c.Id == categoryId);

            if (category != null)
            {
                return category;
            }

            throw new Exception($"No category found with ID: {categoryId}");
        }

    }
}