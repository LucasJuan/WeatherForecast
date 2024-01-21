using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using WeatherGeocoding.API.Filters;
using WeatherGeoCoding.Configuration;

var builder = WebApplication.CreateBuilder(args);

#region Registration

builder.Services.Register(builder.Configuration);
builder.Services.RegisterCors(builder.Configuration);

#endregion
// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResponseObjectTFilter>();
});

var app = builder.Build();

app.UseCors();

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
