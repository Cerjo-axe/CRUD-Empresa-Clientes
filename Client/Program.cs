using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseurl="http://localhost:5100";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseurl) });
builder.Services.AddScoped<ISegmentoService,SegmentoService>();

await builder.Build().RunAsync();
