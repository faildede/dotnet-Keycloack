using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using eshop_auth.Models;
using eshop_auth.Dal;
using Microsoft.OpenApi.Models;
using eshop_auth.Auth.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddSwaggerGenWithAuth(builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.Audience = builder.Configuration["Authentication:Audience"];
        o.MetadataAddress = builder.Configuration["Authentication:MetadataAddress"]!;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Authentication:ValidateIssuer"],
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "eshop_auth API v1");
        c.RoutePrefix = string.Empty; 
        c.OAuthClientId("public-client"); 
        c.OAuthAppName("eshop_auth API");
        c.OAuthUsePkce();
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();
app.UseAuthentication();


app.Run();
