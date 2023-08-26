using Microsoft.EntityFrameworkCore;
using Places_Service.Data;
using Places_Service.Repository;
using Places_Service.Repository.Interfaces;
using Places_Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IGooglePlaceService, GooglePlaceService>();
builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("PlacesDb");
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