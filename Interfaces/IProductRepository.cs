using Microsoft.AspNetCore.Mvc;
using NotesApi.Models;
using ProductManagement.Dtos;
using ProductManagement.Models;

namespace ProductManagement.Interfaces;

public interface IProductRepository : IBaseRepository{
    public IEnumerable<ProductDto> GetAllProducts();
    public Product GetSingleProduct(int productId);
}