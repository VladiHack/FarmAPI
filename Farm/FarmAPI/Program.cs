using Microsoft.EntityFrameworkCore;
using FarmAPI.Models;
using FarmAPI.Services.Animals;
using FarmAPI.Services.Employees;
using FarmAPI.AutoMapper;
using FarmAPI.Services.Crops;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FarmDBContext>();

builder.Services.AddAutoMapper(typeof(AnimalProfile), typeof(EmployeeProfile), typeof(CropProfile));

builder.Services.AddTransient<IAnimalService, AnimalService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<ICropsService, CropsService>();
// Register the FarmDBContext using the connection string from appsettings.json

builder.Services.AddDbContext<FarmDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


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