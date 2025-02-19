using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(); // Enables controllers for API endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Enables Swagger for API testing

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(); // Enables Swagger UI for easy API testing
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization(); // Enable Authorization (optional, can be expanded later)

app.MapControllers(); // Maps all API controllers

app.Run();
