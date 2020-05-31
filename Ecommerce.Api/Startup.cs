using System;
using AutoMapper;
using Ecommerce.AutoMapper.Profiles;
using Ecommerce.Helpers;
using Ecommerce.Models.Settings;
using Ecommerce.Repository;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service;
using Ecommerce.Service.Interfaces;
using Ecommerce.SqlData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Ecommerce.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlDbContext")));
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddAutoMapper(typeof(BaseProfile));

            var contactInfo = Configuration.GetSectionModel<ContactInfo>();
            var documentation = Configuration.GetSectionModel<Documentation>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = documentation.Title,
                    Version = "v1",
                    Description = documentation.Description,
                    Contact = new OpenApiContact
                    {
                        Name = contactInfo.Name,
                        Email = contactInfo.EmailAddress,
                        Url = new Uri(contactInfo.LinkedinUrl)
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            var documentation = Configuration.GetSectionModel<Documentation>();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", documentation.Title);
                // To serve the Swagger UI at the app's root 
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
