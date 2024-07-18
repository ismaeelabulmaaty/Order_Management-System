using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Order_Manag.Core.Entites.Identity;
using Order_Manag.Core.ServicesContract;
using Order_Manag.Services;
using Oredr_Manag.Repository.Identity;
using System.Text;

namespace Order_Management.ExtensionsMethod
{
    public static class IdentityServicesExtension
    {

        public static IServiceCollection AddIdentityService(this IServiceCollection Services, IConfiguration configuration)
        {

            Services.AddScoped<ITokenService, TokenService>();
            Services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>();

            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = configuration["JWT:ValidIssured"],
                            ValidateAudience = true,
                            ValidAudience = configuration["JWT:ValidAudience"],
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty)),
                        };
                    });

            return Services;
        }
    }
}
