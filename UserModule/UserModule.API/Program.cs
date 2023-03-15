using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.ExternalConnectors;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using StockModule.DAL.Services;
using UserModule.BLL.Helpers;
using UserModule.BLL.Interfaces;
using UserModule.BLL.Logic;
using UserModule.DAL;
using UserModule.DAL.Services;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

var mapperConfiguration = new MapperConfiguration(cfg =>
{
    var mapperProfile = new AutoMapperProfile();
    cfg.AddProfile(mapperProfile);
});
var mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.

//AzureAdServices
builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration)
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddMicrosoftGraph(builder.Configuration.GetSection("GraphAPI"))
    .AddInMemoryTokenCaches();



builder.Services.AddControllers();

builder.Services.AddScoped<IPermissionLogic, PermissionLogic>();

builder.Services.AddScoped<IPermissionService, PermissionService>();

// Swagger
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
    }) ;

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

//DBcontest
SqlConnectionStringBuilder sqlbuilder = new SqlConnectionStringBuilder((builder.Configuration.GetConnectionString("UserModuleConnectionString")));
//sqlbuilder.Password = TrippleDES.Decrypt(TrippleDES.GetBytes(sqlbuilder.Password.ToString()));
SqlConnection dbCon = new SqlConnection(sqlbuilder.ConnectionString);
builder.Services.AddDbContext<UserModuleDdContext>(options => options.UseSqlServer(dbCon));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId(builder.Configuration["SwaggerAzureAd:ClientId"]);
        c.OAuthUsePkce();
        c.OAuthScopeSeparator(" ");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
