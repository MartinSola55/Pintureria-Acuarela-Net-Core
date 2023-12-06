using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinturería_Acuarela.Data.Repository.IRepository;
using Pinturería_Acuarela.Models;

namespace Pinturería_Acuarela.Data.Repository
{
    public class ApplicationUserRepository(ApplicationDbContext db) : Repository<ApplicationUser>(db), IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db = db;
    }
}