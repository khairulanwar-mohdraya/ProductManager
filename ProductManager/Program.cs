using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using ProductManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoDBConfig>(builder.Configuration.GetSection("MongoDBConfig"));
builder.Services.AddScoped<ProductService>();
//builder.Services.AddControllers();
builder.Services.AddMvcCore();
builder.Services.AddControllersWithViews();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
