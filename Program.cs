using BlazorApp_PlatziCourse;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var apiURL = builder.Configuration.GetValue<string>("apiURL");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiURL) });

await builder.Build().RunAsync();
