using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BackendApi.Auth;
using BackendApi.Auth.Handlers;
using BackendApi.Auth.Models;
using BackendApi.Data;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers;
using BackendApi.Helpers.Services;
using BackendApi.Helpers.Sorting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<ShopUser, IdentityRole>()
    .AddEntityFrameworkStores<ShopDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters.ValidAudience = builder.Configuration["JWT:ValidAudience"];
        options.TokenValidationParameters.ValidIssuer = builder.Configuration["JWT:ValidIssuer"];
        options.TokenValidationParameters.IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]));
    });

builder.Services.AddDbContext<ShopDbContext>();
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();

builder.Services.AddTransient<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddScoped<AuthDbSeeder>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyNames.ResourceOwner, policy => policy.Requirements.Add(new ResourceOwnerRequirement()));
    options.AddPolicy(PolicyNames.SellerShopOwner, policy => policy.Requirements.Add(new SellerShopOwnerRequirement()));
});

builder.Services.AddSingleton<IAuthorizationHandler, ResourceOwnerAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, SellerShopOwnerAuthorizationHandler>();

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// 
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
dbContext.Database.Migrate();

var dbSeeder = scope.ServiceProvider.GetRequiredService<AuthDbSeeder>();
await dbSeeder.SeedAsync();

app.Run();