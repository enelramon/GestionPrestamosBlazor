
global using Microsoft.EntityFrameworkCore;
global using GestionPrestamos.Models;
global using GestionPrestamos.Components;
global using GestionPrestamos.Context;
global using GestionPrestamos.Services;
global using GestionPrestamos.Controllers;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
//builder.Services.AddDbContext<Contexto>(o => o.UseSqlServer(ConStr));
builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConStr));


builder.Services.AddScoped<PrestamosService>();
builder.Services.AddScoped<DeudoresService>();
builder.Services.AddScoped<CobrosService>();

// Inyeccion del servicio de Bootstrap
builder.Services.AddBlazorBootstrap();


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();


app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
