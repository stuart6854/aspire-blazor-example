﻿using AspireBlazorApp.BL.Services;
using AspireBlazorApp.Models.Entities;
using AspireBlazorApp.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspireBlazorApp.ApiService.Controllers;

[Authorize(Roles = "Admin,User")]
[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel>> GetProducts()
    {
        var products = await productService.GetProducts();
        return Ok(new BaseResponseModel { Success = true, Data = products });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BaseResponseModel>> GetProduct(int id)
    {
        var productModel = await productService.GetProduct(id);
        if (productModel == null)
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "Not found" });
        }

        return Ok(new BaseResponseModel { Success = true, Data = productModel });
    }

    [HttpPost]
    public async Task<ActionResult<ProductModel>> CreateProduct(ProductModel productModel)
    {
        await productService.CreateProduct(productModel);
        return Ok(new BaseResponseModel { Success = true });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductModel productModel)
    {
        if (id != productModel.Id || !await productService.ProductModelExists(id))
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "Bad request" });
        }

        await productService.UpdateProduct(productModel);
        return Ok(new BaseResponseModel { Success = true });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        if (!await productService.ProductModelExists(id))
        {
            return Ok(new BaseResponseModel { Success = false, ErrorMessage = "Not found" });
        }

        await productService.DeleteProduct(id);
        return Ok(new BaseResponseModel { Success = true });
    }
}