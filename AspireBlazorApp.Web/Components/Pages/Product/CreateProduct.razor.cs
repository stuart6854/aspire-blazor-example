using AspireBlazorApp.Models.Entities;
using AspireBlazorApp.Models.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;

namespace AspireBlazorApp.Web.Components.Pages.Product;

public partial class CreateProduct
{
	[Inject]
	private ApiClient ApiClient { get; set; }
	[Inject]
	private NavigationManager NavigationManager { get; set; }
	[Inject]
	private IToastService ToastService { get; set; }

	public ProductModel Model { get; set; } = new();

	public async Task Submit()
	{
		var res = await ApiClient.PostAsync<BaseResponseModel, ProductModel>("/api/product", Model);
		if (res is { Success: true })
		{
			ToastService.ShowSuccess("Create product successfully");
			NavigationManager.NavigateTo("/product");
		}
	}
}
