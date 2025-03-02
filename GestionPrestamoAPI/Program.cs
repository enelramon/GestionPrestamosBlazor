using GestionPrestamos.Context;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var corsPolicy = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
//builder.Services.AddDbContext<Contexto>(o => o.UseSqlServer(ConStr));
builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConStr));




builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,
        policy => policy.WithOrigins("https://localhost:7050") 
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{   
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(); 
    app.MapOpenApi();
}



app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
