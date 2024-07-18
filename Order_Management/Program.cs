using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order_Manag.Core.Entites.Identity;
using Order_Manag.Core.Repository.Contract;
using Order_Management.ExtensionsMethod;
using Order_Management.HandlingErrors;
using Order_Management.Helpers;
using Order_Management.MiddleWare;
using Oredr_Manag.Repository.Data;
using Oredr_Manag.Repository.Identity;
using Oredr_Manag.Repository.ImplementRepository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ManagDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
});

builder.Services.AddApplicationServices();
builder.Services.AddIdentityService(builder.Configuration);


#endregion

var app = builder.Build();

#region UpdateDataBase

using
var Scope = app.Services.CreateScope();
var Service = Scope.ServiceProvider;
var LoggerFactory = Service.GetService<ILoggerFactory>();


try
{
    var DbContext = Service.GetRequiredService<ManagDbContext>();
    await DbContext.Database.MigrateAsync(); //update database
                                             //Scope.Dispose();

    var IdentityDbContext = Service.GetRequiredService<AppIdentityDbContext>();
    await IdentityDbContext.Database.MigrateAsync();

    var userManager = Service.GetRequiredService<UserManager<User>>();
    await AppIdentityDbContextSeed.SeedUserAsync(userManager);

}
catch (Exception ex)
{

    var Logger = LoggerFactory.CreateLogger<Program>();
    Logger.LogError(ex, "An Error Occurde During Appling The Migration");
}

#endregion


#region Middle Ware__Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<ExceptionMiddleWare>();


    app.UseSwaggerMiddleWare();
}
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();
