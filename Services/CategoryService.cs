using BlazorApp_PlatziCourse.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp_PlatziCourse.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly HttpClient client;
		private readonly JsonSerializerOptions options;
		public CategoryService(HttpClient _client)
		{
			client = _client;
			options = options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}

		public async Task<List<Category>?> GetCategories()
		{
			//var response = await client.GetAsync("v1/categories");
			//return await JsonSerializer.DeserializeAsync<List<Category>>(await response.Content.ReadAsStreamAsync());

            var response = await client.GetAsync("v1/categories");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            return JsonSerializer.Deserialize<List<Category>>(content, options);
        }

		public async Task<Category?> GetCategory(int categoryId)
		{
			var response = await client.GetAsync($"v1/categories/{categoryId}");
			return await JsonSerializer.DeserializeAsync<Category>(await response.Content.ReadAsStreamAsync());
		}
	}

	public interface ICategoryService
	{
		Task<List<Category>?> GetCategories();
		Task<Category?> GetCategory(int categoryId);
	}
}
