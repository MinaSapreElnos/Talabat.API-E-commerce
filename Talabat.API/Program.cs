using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Talabat.API.Helpers;
using Talabat.Core;
using Talabat.Core.IGenericRepository;
using Talabat.Core.Models.Identity;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace Talabat.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configer Services 

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            //builder.Services.AddSwaggerGen(C =>
            //{
            //    C.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        Name = "Authentication",

            //        Type = SecuritySchemeType.Http,

            //        Scheme = "Bearer",

            //        In = ParameterLocation.Header,
            //    });

                //C.AddSecurityRequirement(new OpenApiSecurityRequirement {
                //{
                //    new OpenApiSecurityScheme
                //    {
                //        Reference = new OpenApiReference
                //        {
                //            Id = "Authentication",
                //            Type = ReferenceType.SecurityScheme
                //        }
                //    },new List<string>()

                //}
                //});
            //});







            builder.Services.AddDbContext<StoreContext>(option =>
            option.UseSqlServer( builder.Configuration.GetConnectionString("default Connection") ) );


            builder.Services.AddDbContext<AppIdentityDbContext>(obtion =>
                obtion.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));

            builder.Services.AddScoped(typeof(IprodectServise), typeof(ProdectServise));

            builder.Services.AddScoped(typeof(IAuthServise),typeof(AuthServise));
            builder.Services.AddScoped(typeof(IPaymentService), typeof(PaymentService));

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,

                        ValidAudience = builder.Configuration["Jwt:ValiedAudience"],

                        ValidateIssuer = true,

                        ValidIssuer = builder.Configuration["Jwt:issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecritKey"])),

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.FromDays(double.Parse(builder.Configuration["Jwt:DurationDay"]))
                        

                    };
                });

            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddAutoMapper(typeof(MapingProfile));

            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.Password.RequireUppercase = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;

            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            builder.Services.AddSingleton<IConnectionMultiplexer>((servieceProvider) =>
            {
                var Connection = builder.Configuration.GetConnectionString("Redis"); 

                return ConnectionMultiplexer.Connect(Connection); 
            });
                
                   
            #endregion

             var app = builder.Build();


            var Scope = app.Services.CreateScope();

            var Services = Scope.ServiceProvider;

            var _DbContext = Services.GetRequiredService<StoreContext>();

            var IdentityDbContext = Services.GetRequiredService<AppIdentityDbContext>();

            var LoggerFactory = Services .GetRequiredService<ILoggerFactory>();

            try
            {
               await  _DbContext.Database.MigrateAsync();

                await StoreContaxtSeed.SeedAsync(_DbContext); 

                await IdentityDbContext.Database.MigrateAsync();

                var _userManeger = Services.GetRequiredService<UserManager<AppUser>>();

                await AppIdentityDbContextSead.SeadUserAsync(_userManeger);
            }
            catch ( Exception ex ) 
            {

                var Logger =LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex,"an error has been occured during apply the migration");

            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();
             
            app.MapControllers();

            app.Run();
        }
    }
}