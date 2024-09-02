using AspireBlazorApp.Models.Entities;
using AspireBlazorApp.Models.Models;
using AspireBlazorApp.Web.Components.BaseComponent;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace AspireBlazorApp.Web.Components.Pages.Product;

public partial class IndexProduct
{
    [Inject]
    public ApiClient ApiClient { get; set; }
    [Inject]
    public IToastService ToastService { get; set; }

    public List<ProductModel> ProductsModels { get; set; }

    public AppModal Modal { get; set; }
    public int DeleteId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await LoadProducts();
    }

    protected async Task LoadProducts()
    {
        var res = await ApiClient.GetFromJsonAsync<BaseResponseModel>("/api/product");
        if (res is { Success: true })
        {
            ProductsModels = JsonConvert.DeserializeObject<List<ProductModel>>(res.Data.ToString());
            StateHasChanged();
        }
    }

    protected async void HandleDelete()
    {
        var res = await ApiClient.DeleteAsync<BaseResponseModel>($"/api/product/{DeleteId}");
        if (res is { Success: true })
        {
            ToastService.ShowSuccess("Deleted product successfully");
            await LoadProducts();
            Modal.Close();
        }
    }
}