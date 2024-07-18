using Microsoft.AspNetCore.Identity;
using Order_Manag.Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oredr_Manag.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {

        public static async Task SeedUserAsync(UserManager<User> _userManager)
        {
            if(!_userManager.Users.Any())
            {
                var Users = new User()
                {
                    DisplayName = "ismaeel abulmaaty",
                    Email = "ismaeelmatty@gmail.com",
                    UserName = "ismaeelmatty",
                    PhoneNumber = "01003793959"
                    
                    



                };
                await _userManager.CreateAsync(Users, "Pa$$w0rd");
            }
          
           
        }

    }
}
