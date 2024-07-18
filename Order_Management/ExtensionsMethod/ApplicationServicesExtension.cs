using Microsoft.AspNetCore.Mvc;
using Order_Manag.Core.Repository.Contract;
using Order_Manag.Services;
using Order_Management.HandlingErrors;
using Order_Management.Helpers;
using Oredr_Manag.Repository.ImplementRepository;

namespace Order_Management.ExtensionsMethod
{
    public static class ApplicationServicesExtension
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<IPaymentService, PaymentSerive>();
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            Services.AddAutoMapper(typeof(MappingProfiles));

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                       .SelectMany(p => p.Value.Errors)
                                                       .Select(E => E.ErrorMessage)
                                                       .ToList();


                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };



            });


            return Services;

        }
    }
}
