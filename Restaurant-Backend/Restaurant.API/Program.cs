using Microsoft.EntityFrameworkCore;
using Restaurant.API.Helper;
using Restaurant.API.Interface;
using Restaurant.API.Service;
using Restaurant.Data;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<RestaurantDbContext>(opt => opt.UseLazyLoadingProxies().UseInMemoryDatabase(databaseName: "Restaurant"));
builder.Services.AddScoped<RestaurantDbContext>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITableService, TableService>();


using (ServiceProvider serviceProvider = builder.Services.BuildServiceProvider())
{
    DataGenerator.Initialize(serviceProvider);
}
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        MyAllowSpecificOrigins,
                        builder =>
                        {
                            builder
                                .WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
