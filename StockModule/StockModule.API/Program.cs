using AutoMapper;
using Gelf.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using StockModule.DAL.Services;
using StockModule.BLL;
using StockModule.BLL.Helpers;
using StockModule.BLL.StockSettings;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.Identity.Web;
using StockModule.DAL;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Mapper
var mapperConfiguration = new MapperConfiguration(cfg =>
{
    var mapperProfile = new AutoMapperProfile();
    cfg.AddProfile(mapperProfile);
});
var mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region AzureAdServices


builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration)
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddMicrosoftGraph(builder.Configuration.GetSection("GraphAPI"))
    .AddInMemoryTokenCaches();

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger
// 
builder.Services.AddSwaggerGen(c =>
{
    //c.SwaggerDoc("v1", new OpenApiInfo
    //{
    //    Version = "v1",
    //    Title = "Stock Module Api",
    //});
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "OAuth2 which uses AuthorizationCode flow and Azure AD",
        Name = "oauth2",
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(builder.Configuration["SwaggerAzureAd:AuthorizationUrl"]),
                TokenUrl = new Uri(builder.Configuration["SwaggerAzureAd:TokenUrl"]),
                Scopes = new Dictionary<string, string>
                {
                    { builder.Configuration["SwaggerAzureAd:Scopes"],"Access API as User" }
                }
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new[] {  builder.Configuration["SwaggerAzureAd:Scopes"] }
        }
    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "User Module Api",
    });
});
/*
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "OAuth2 which uses AuthorizationCode flow and Azure AD",
        Name = "oauth2",
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(builder.Configuration["SwaggerAzureAd:AuthorizationUrl"]),
                TokenUrl = new Uri(builder.Configuration["SwaggerAzureAd:TokenUrl"]),
                Scopes = new Dictionary<string, string>
                {
                    { builder.Configuration["SwaggerAzureAd:Scopes"],"Access API as User" }
                }
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new[] {  builder.Configuration["SwaggerAzureAd:Scopes"] }
        }
    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "User Module Api",
    });
});
*/


#endregion

#region Logger
builder.Logging.AddGelf(options =>
{
    options.LogSource = Assembly.GetEntryAssembly()?.GetName().Name;
    options.AdditionalFields["machine_name"] = Environment.MachineName;
    options.AdditionalFields["app_version"] = Assembly.GetEntryAssembly()?
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "";
});
#endregion Logger

#region EntityServices
builder.Services.ConfigureDALServices(builder.Configuration);
#endregion EntityServices

#region BusinessServices
builder.Services.ConfigureBLLServices(builder.Configuration);
#endregion BusinessServices

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.OAuthClientId(builder.Configuration["SwaggerAzureAd:ClientId"]);
    c.OAuthUsePkce();
    c.OAuthScopeSeparator(" ");
});
/* app.UseSwaggerUI(c =>
 {
     c.OAuthClientId(builder.Configuration["SwaggerAzureAd:ClientId"]);
     c.OAuthUsePkce();
     c.OAuthScopeSeparator(" ");
 });*/
//}
//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Use CORS
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();