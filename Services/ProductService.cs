using BlazorApp_PlatziCourse.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp_PlatziCourse.Services
{
	public class ProductService
	{
		private readonly HttpClient client;
		private readonly JsonSerializerOptions options;
		public ProductService(HttpClient _client, JsonSerializerOptions _options) 
		{
			client = _client;
			options = _options;
		}

		public async Task<List<Product>?> GetProducts() 
		{
			var response = await client.GetAsync("v1/products");
			return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
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

	}
}
