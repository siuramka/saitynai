using BackendApi.Data;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShopDbContext>();
builder.Services.AddTransient<IShopRepository, ShopRepository>();
builder.Services.AddTransient<ISoftwareRepository, SoftwareRepository>();
builder.Services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();

builder.Services.AddTransient<ISubscriptionService, SubscriptionService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
dbContext.Database.Migrate();

app.Run();