using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pintureria_Acuarela.Data.Repository.IRepository;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Data.Repository
{
    public class ApplicationUserRepository(ApplicationDbContext db) : Repository<ApplicationUser>(db), IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db = db;
        public IdentityRole GetRole(string userID)
        {
            return _db.Roles.FirstOrDefault(x => x.Id.Equals(_db.UserRoles.FirstOrDefault(x => x.UserId.Equals(userID)).RoleId));
        }
    }
}