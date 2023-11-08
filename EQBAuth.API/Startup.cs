using System;
using System.IO;
using EQBAuth.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace EQBAuth.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy", builder =>
            //        builder.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials()
            //        .Build());
            //});
            var origins = Configuration["AllowedOrigins"].Split(";");
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins(origins) 
                    .Build());
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddControllers();
            services.AddScoped<IAuthService, AuthService>(); 
           
            services.AddDistributedMemoryCache();
            services.AddHealthChecks();
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //app.UseSession();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseMvc();
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHealthChecks("/health");
 
        }
    }
}


 