using AspireBlazorApp.BL.Repositories;
using AspireBlazorApp.Models.Entities;

/*
 * Service = Business logic
 */

namespace AspireBlazorApp.BL.Services;

public interface IProductService
{
    Task<bool> ProductModelExists(int id);
    Task<List<ProductModel>> GetProducts();
    Task<ProductModel> GetProduct(int id);
    Task<ProductModel> CreateProduct(ProductModel productModel);
    Task UpdateProduct(ProductModel productModel);
    Task DeleteProduct(int id);
}

public class ProductService(IProductRepository productRepository) : IProductService
{
    public Task<bool> ProductModelExists(int id)
    {
        return productRepository.ProductModelExists(id);
    }

    public Task<List<ProductModel>> GetProducts()
    {
        return productRepository.GetProducts();
    }

    public Task<ProductModel> GetProduct(int id)
    {
        return productRepository.GetProduct(id);
    }

    public Task<ProductModel> CreateProduct(ProductModel productModel)
    {
        return productRepository.CreateProduct(productModel);
    }

    public Task UpdateProduct(ProductModel productModel)
    {
        return productRepository.UpdateProduct(productModel);
    }

    public Task DeleteProduct(int id)
    {
        return productRepository.DeleteProduct(id);
    }
}