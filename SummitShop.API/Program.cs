
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using SummitShop.API.Errors;
using SummitShop.API.Extensions;
using SummitShop.API.Helpers;
using SummitShop.API.Middlewares;
using SummitShop.Core.Entities;
using SummitShop.Core.Entities.Identity;
using SummitShop.Core.Repositories;
using SummitShop.Repository.Data;
using SummitShop.Repository.Identity;
using SummitShop.Repository.RepositoryImplementation;

namespace SummitShop.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // builder here is the web application builder (kestral or iis builder)
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerService();

            builder.Services.AddDbContext<SummitDbContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
                      options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connectionString);
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                #region Validations
                //options.Password.RequireDigit = false;
                //options.Password.RequireLowercase = false;
                //options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequireUppercase = false;
                //options.Password.RequiredLength = 6; // Adjust as needed 
                #endregion
            })
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddDefaultTokenProviders();
            //.AddDefaultTokenProviders();


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();


            //ApplicationServicesExtension.AddApplicationService(builder.Services);
            builder.Services.AddApplicationService();



            // app is the kestral
            var app = builder.Build();



            // here i want to ask the clr to get all scoped services and get the service of summitdbcontext to make anew instance
            // of it to migrate the database always when the application will be run 

            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var Logger = services.GetRequiredService<ILogger<Program>>();


            try
            {
                var context = services.GetRequiredService<SummitDbContext>();
                await context.Database.MigrateAsync();
                await ContextSeedData.SeedDataAsync(context);

                var identityContext = services.GetRequiredService<ApplicationIdentityDbContext>();
                await identityContext.Database.MigrateAsync(); // update database (database.savechanges) after add migration

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                await AppIdentitySeeding.SeedUsersAsync(userManager);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occured while migrating process");

            }


            app.UseMiddleware<ExceptionMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.UseSwaggerMiddleware();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
