using Microsoft.EntityFrameworkCore;
using Places.Data;
using Places.Repository;
using Places.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PlaceDbContext>(options =>
{
    options.UseInMemoryDatabase("InMem");
});
// builder.Services.AddMassTransit(x =>
// {
//     x.UsingRabbitMq((context, cfg) =>
//     {
//         cfg.Host("localhost", 5672, "/", h =>
//         {
//             h.Username("guest");
//             h.Password("guest");
//         });
//     });
// });
builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();
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
