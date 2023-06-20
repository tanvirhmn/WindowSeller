using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Gelf.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MudBlazor.Services;
using Radzen;
using StockModule.UI.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


//var initialScopes = builder.Configuration["DownstreamApi:Scopes"]?.Split(' ') ?? configuration["MicrosoftGraph:Scopes"]?.Split(' ');

//// Add services to the container.
//builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApp(configuration.GetSection("AzureAd"))
//    .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
//    .AddDownstreamApi("DownstreamApi", configuration.GetSection("DownstreamApi"))
//    .AddMicrosoftGraph(configuration.GetSection("MicrosoftGraph"))    
//    .AddInMemoryTokenCaches();

//builder.Services.AddControllersWithViews()
//    .AddMicrosoftIdentityUI();

//builder.Services.AddAuthorization(options =>
//{
//    // By default, all incoming requests will be authorized according to the default policy
//    options.FallbackPolicy = options.DefaultPolicy;
//});


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
                .AddMicrosoftIdentityConsentHandler();
builder.Services.AddMudServices();
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddSingleton<StockAccountingService>();
builder.Services.AddSingleton<StockSettingsMaterial_FolderHierarchyService>();
builder.Services.AddSingleton<FolderHierarchyService>();
builder.Services.AddSingleton<StockSettingsService>();



#region Logger
builder.Logging.AddGelf(options =>
{
    options.LogSource = Assembly.GetEntryAssembly()?.GetName().Name;
    options.AdditionalFields["machine_name"] = Environment.MachineName;
    options.AdditionalFields["app_version"] = Assembly.GetEntryAssembly()?
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "";
});
#endregion Logger

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(configuration["StockModule:ApiUrl"]!) });





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
