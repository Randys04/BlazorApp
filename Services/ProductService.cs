using BlazorApp_PlatziCourse.Models;
using BlazorApp_PlatziCourse.Pages.Products;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp_PlatziCourse.Services
{
	public class ProductService : IProductService
	{
		private readonly HttpClient client;
		private readonly JsonSerializerOptions options;
		public ProductService(HttpClient _client) 
		{
			client = _client;
			options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
		}

		public async Task<List<Product>?> GetProducts() 
		{
			var response = await client.GetAsync("v1/products");
			var content = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}

			return JsonSerializer.Deserialize<List<Product>>(content, options);
		}

        public async Task<Product?> GetProduct(int productid)
        {
            var response = await client.GetAsync($"v1/products/{productid}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            return JsonSerializer.Deserialize<Product>(content, options);
        }

        public async Task AddProducts(Product product)
		{
			var response = await client.PostAsync("v1/products", JsonContent.Create(product));
			var content = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode) 
			{
				throw new ApplicationException(content);
			}
		}

		public async Task DeleteProducts(int productId)
		{
			var response = await client.DeleteAsync($"v1/products/{productId}");
			var content = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}
		}

        public async Task EditProducts(Product product)
        {
            var response = await client.PutAsync($"v1/products/{product.Id}", JsonContent.Create(product));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }

    }

	public interface IProductService 
	{
		Task<List<Product>?> GetProducts();
		Task<Product?> GetProduct(int productid);
        Task AddProducts(Product product);
		Task DeleteProducts(int productId);
		Task EditProducts(Product product);

    }
}
