using GestionPrestamos.Components;
using GestionPrestamos.Context;
using GestionPrestamos.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Inyeccion del contexto
var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
//builder.Services.AddDbContext<Contexto>(o => o.UseSqlServer(ConStr));
builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConStr));

//Inyeccion del service
builder.Services.AddScoped<PrestamosService>();
builder.Services.AddScoped<DeudoresService>();
builder.Services.AddScoped<CobrosService>();

builder.Services.AddBlazorBootstrap();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
