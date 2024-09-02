using AspireBlazorApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspireBlazorApp.Database.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
		Database.EnsureCreated();
	}

	public DbSet<ProductModel> Products { get; set; }
}
