using Microsoft.EntityFrameworkCore;
using Reviews.Data;
using Reviews.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDatabase"));
builder.Services.AddSingleton<MongoReviewService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ReviewDbContext>(options =>
    {
        options.UseInMemoryDatabase("InMemory");
    });
}

if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<ReviewDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ReviewMSSQL"));
    });
}

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