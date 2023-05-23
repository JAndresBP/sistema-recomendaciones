using Redis.OM;
using servicio_recomendaciones.Domain.Services;

var redisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") ?? "localhost:6379";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHostedService<IndexCreationService>();
builder.Services.AddHostedService<Worker>();
builder.Services.AddHttpClient<RecomendationService>();
builder.Services.AddSingleton(new RedisConnectionProvider($"redis://{redisConnectionString}"));

builder.Services.AddControllers();
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
