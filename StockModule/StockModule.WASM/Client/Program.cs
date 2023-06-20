using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Radzen;
using StockModule.UI.Data;
using StockModule.WASM;

namespace StockModule.WASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddMudServices();

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            builder.Services.AddSingleton<StockAccountingService>();
            builder.Services.AddSingleton<StockSettingsMaterial_FolderHierarchyService>();
            builder.Services.AddSingleton<FolderHierarchyService>();
            builder.Services.AddSingleton<StockSettingsService>();
            var apiBaseAddress = "http://localhost:5096/api/";
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(apiBaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}