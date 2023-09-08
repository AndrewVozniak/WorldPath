using Microsoft.AspNetCore.WebSockets;
using Places_Service.Data;
using Places_Service.Hubs;
using Places_Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDatabaseSettings>(builder.Configuration.GetSection("MongoDatabase"));
builder.Services.AddSingleton<PlaceService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

builder.Services.AddSignalR(options =>
{
    options.HandshakeTimeout = TimeSpan.FromSeconds(10);
    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
    options.EnableDetailedErrors = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapHub<PlaceHub>("/place-hub");

app.MapControllers();

app.Run();