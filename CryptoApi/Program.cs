using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Map controllers
app.MapControllers();

// Health & basic endpoints
app.MapGet("/", () => Results.Ok("OK"));
app.MapGet("/health", () => Results.Ok("Healthy"));
app.MapGet("/version", () => Results.Ok("v1.0"));

// IMPORTANT
app.Run();