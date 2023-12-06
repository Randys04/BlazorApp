using BlazorApp_PlatziCourse.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp_PlatziCourse.Services
{
	public class CategoryService
	{
		private readonly HttpClient client;
		private readonly JsonSerializerOptions options;
		public CategoryService(HttpClient _client, JsonSerializerOptions _options)
		{
			client = _client;
			options = _options;
		}

		public async Task<List<Category>?> GetCategory()
		{
			var response = await client.GetAsync("v1/categories");
			return await JsonSerializer.DeserializeAsync<List<Category>>(await response.Content.ReadAsStreamAsync());
		}
	}
}
