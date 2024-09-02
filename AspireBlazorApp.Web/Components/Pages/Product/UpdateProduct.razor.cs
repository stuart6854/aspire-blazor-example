using AspireBlazorApp.Models.Entities;
using AspireBlazorApp.Models.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace AspireBlazorApp.Web.Components.Pages.Product;

public partial class UpdateProduct : ComponentBase
{
	[Inject]
	private ApiClient ApiClient { get; set; }
	[Inject]
	private NavigationManager NavigationManager { get; set; }
	[Inject]
	private IToastService ToastService { get; set; }

	[Parameter]
	public int Id { get; set; }
	public ProductModel Model { get; set; } = new();

	protected override async Task OnInitializedAsync()
	{
		await base.OnInitializedAsync();
		var res = await ApiClient.GetFromJsonAsync<BaseResponseModel>($"/api/product/{Id}");
		if (res is { Success: true })
		{
			Model = JsonConvert.DeserializeObject<ProductModel>(res.Data.ToString());
		}
	}

	public async Task Submit()
	{
		var res = await ApiClient.PutAsync<BaseResponseModel, ProductModel>($"/api/product/{Id}", Model);
		if (res is { Success: true })
		{
			ToastService.ShowSuccess("Updated product successfully");
			NavigationManager.NavigateTo("/product");
		}
	}
}
