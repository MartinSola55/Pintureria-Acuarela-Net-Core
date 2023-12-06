using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pinturería_Acuarela.Models;
using System.Data.Common;
using System.Reflection.Emit;

namespace Pinturería_Acuarela.Data.Seeding
{
    public class Seeder : ISeeder
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly UserManager<IdentityRole> _roleManager;

        public Seeder(ApplicationDbContext db, IConfiguration config, UserManager<IdentityUser> userManager/*, UserManager<IdentityRole> roleManager*/)
        {
            _db = db;
            _config = config;
            _userManager = userManager;
            //_roleManager = roleManager;
        }
        public void Seed()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }

            // Crear roles
            //_roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();

            // Crear usuarios
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = _config["User:Name"],
                Email = _config["User:Email"],
                EmailConfirmed = true,
                BusinessID = 1,
                
            }, 
            _config["User:Password"]).GetAwaiter().GetResult();

            //IdentityUser user = _db.Users.Where(x => x.Email == "email").FirstOrDefault();
            //_userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
        }
    }
}
