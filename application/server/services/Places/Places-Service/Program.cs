using AutoMapper;
using Places_Service.Services;
using Places.Application;
using Places.Application.Interfaces;
using Places.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddHttpClient<IGooglePlaceService, GooglePlaceService>();
builder.Services.AddCors(options =>  
{  
    options.AddPolicy("AllowAll", policy =>  
    {  
        policy.AllowAnyHeader();  
        policy.AllowAnyMethod();  
        policy.AllowAnyOrigin();  
    });  
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

app.UseCors("AllowAll");

app.MapControllers();

app.Run();