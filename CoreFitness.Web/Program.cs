using CoreFitness.Application;
using CoreFitness.Application.Interfaces;
using CoreFitness.Application.Services;
using CoreFitness.Domain.Entities;
using CoreFitness.Domain.Identity;
using CoreFitness.Domain.Repositoryes.Members;
using CoreFitness.Infrastructure.Persistence.Contexts;
using CoreFitness.Infrastructure.Repositories;
using CoreFitness.Infrastructure.Repositories.Members;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);





// Registrerar Identity-systemet så att SignInManager kan användas
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
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

// 1. Registrera Repositoryt (Hjärnan som pratar med databasen)
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

// 2. Registrera Servicen (Hjärnan som håller i logiken)
builder.Services.AddScoped<IGymService, GymService>();


builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new InvalidOperationException("Google ClientId is not configured.");
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new InvalidOperationException("Google ClientSecret is not configured.");
    });

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



 static void SeedDatabase(DataContext context)
{
    if (!context.GymSessions.Any()) // Om tabellen är helt tom
    {
        context.GymSessions.AddRange(new List<GymSession>
        {
            new GymSession { Name = "Padel Nybörjare", Category = "Padel", Description = "Tränna Padel med coach"},
            new GymSession { Name = "PT Pass", Category = "Personal Training", Description = "Öva fina rörelser" },
            new GymSession { Name = "Yoga", Category = "Group Training", Description = "Snabb Tempo Training" }
        });
        context.SaveChanges();
    }
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();
    SeedDatabase(context);
}


app.Run();




