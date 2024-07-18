using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Order_Manag.Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oredr_Manag.Repository.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<User>
    {

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> Options):base(Options)
        {
            
        }

        
    }
}
