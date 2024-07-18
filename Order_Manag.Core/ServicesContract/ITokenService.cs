using Microsoft.AspNetCore.Identity;
using Order_Manag.Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Core.ServicesContract
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User User, UserManager<User> userManager);
    }
}
