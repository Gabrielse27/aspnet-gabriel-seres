using Microsoft.EntityFrameworkCore;
using CoreFitness.Infrastructure.Data;
using CoreFitness.Application.Interfaces;
using CoreFitness.Application;


var builder = WebApplication.CreateBuilder(args);

// 1. Koppla mot databasen med Connection String från appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Registrera din GymService för Dependency Injection (Krav för arkitekturen)
builder.Services.AddScoped<IGymService, GymService>();

// 3. Lägg till stöd för MVC (Controllers och Views)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Standardinställningar för webbservern
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Viktigt för att dina bilder i wwwroot ska synas
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




