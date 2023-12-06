using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pinturería_Acuarela.Data;
using Pinturería_Acuarela.Data.Repository;
using Pinturería_Acuarela.Data.Repository.IRepository;
using Pinturería_Acuarela.Data.Seeding;
using Pinturería_Acuarela.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();
builder.Services.AddControllersWithViews();

// Agregar work container
builder.Services.AddScoped<IWorkContainer, WorkContainer>();

// Agregar seeding
builder.Services.AddScoped<ISeeder, Seeder>();
var users = builder.Configuration["User"];

// Negar nuevos registros
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DisallowRegistration", policy =>
    {
        policy.RequireAuthenticatedUser(); // Requiere que el usuario este autenticado
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

// M�todo para hacer seeding
Seed();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

// Metodo Seed()
void Seed()
{
    using var scope = app.Services.CreateScope();
    var dbSeeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
    dbSeeder.Seed();
}