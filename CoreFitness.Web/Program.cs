using CoreFitness.Application;
using CoreFitness.Application.Interfaces;
using CoreFitness.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using CoreFitness.Domain.Identity;


var builder = WebApplication.CreateBuilder(args);

// Registrerar Identity-systemet så att SignInManager kan användas
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<DataContext>(); // Se till att namnet på din DbContext stämmer

// 1. Koppla mot databasen med Connection String från appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));


// 2. Registrera din GymService för Dependency Injection (Krav för arkitekturen)
builder.Services.AddScoped<IGymService, GymService>();

// 3. Lägg till stöd för MVC (Controllers och Views)
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

var app = builder.Build();

// Standardinställningar för webbservern
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }
    app.UseStatusCodePagesWithReExecute("/Identity/Account/NotFound");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Viktigt för att dina bilder i wwwroot ska synas
app.UseRouting();
app.UseAuthentication();    
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();




