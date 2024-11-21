using Microsoft.AspNetCore.Mvc;
using ProductManagement.Dtos;
using ProductManagement.Models;

namespace ProductManagement.Interfaces;

public interface ICategoryRepsoitory : IBaseRepository
{
    public IEnumerable<Category> GetAllCategories();
    public Category GetSingleCategory(int categoryId);
    public Category GetSingleCategoryWithoutProducts(int categoryId);

    // public IEnumerable<Product> GetProductsB(int categoryId);
    // public IEnumerable<Po> SearchByTitle(string title);
}