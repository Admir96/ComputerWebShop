using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _Config;

        public Startup(IConfiguration config)
        {
            _Config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<ITokenService, TokenService>();
            services.AddControllers();

            services.AddDbContext<DataContext>(options =>
           options.UseSqlServer(_Config.GetSection("ConnectionStrings:DBConn").Value));

            services.AddCors();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
                services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
                
                //  services.AddControllersWithViews();

            
           }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:44452"));

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}