using AspireBlazorApp.Database.Data;
using AspireBlazorApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

/*
 * Repository = Data access
 */

namespace AspireBlazorApp.BL.Repositories;

public interface IProductRepository
{
    Task<bool> ProductModelExists(int id);
    Task<List<ProductModel>> GetProducts();
    Task<ProductModel> GetProduct(int id);
    Task<ProductModel> CreateProduct(ProductModel productModel);
    Task UpdateProduct(ProductModel productModel);
    Task DeleteProduct(int id);
}

public class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public Task<bool> ProductModelExists(int id)
    {
        return dbContext.Products.AnyAsync(p => p.Id == id);
    }

    public Task<List<ProductModel>> GetProducts()
    {
        return dbContext.Products.ToListAsync();
    }

    public Task<ProductModel> GetProduct(int id)
    {
        return dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ProductModel> CreateProduct(ProductModel productModel)
    {
        dbContext.Products.Add(productModel);
        await dbContext.SaveChangesAsync();
        return productModel;
    }

    public async Task UpdateProduct(ProductModel productModel)
    {
        dbContext.Entry(productModel).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteProduct(int id)
    {
        var product = dbContext.Products.FirstOrDefault(p => p.Id == id);
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
    }
}