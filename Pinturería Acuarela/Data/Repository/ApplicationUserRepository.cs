using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturer�a_Acuarela.Data.Repository.IRepository;
using Pinturer�a_Acuarela.Models;

namespace Pinturer�a_Acuarela.Data.Repository
{
    public class ApplicationUserRepository(ApplicationDbContext db) : Repository<ApplicationUser>(db), IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db = db;
    }
}