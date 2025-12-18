using ShelfTagsBE.Data;
using ShelfTagsBE.Repos;
using Microsoft.EntityFrameworkCore;
using ShelfTagsBE.Service;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog before building the app
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("https://localhost4200", "https://localhost4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddMemoryCache();

// Add the DbContext with SQL Server
builder.Services.AddDbContext<DBmodel>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Register repositories and services (DI)
builder.Services.AddScoped<IProductInterface, ProductRepository>();
builder.Services.AddScoped<IShelfTagInterface, ShelfTagRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ShelfTagService>();

// Add controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAngular");

// Map controller routes
app.MapControllers();

app.Run();


