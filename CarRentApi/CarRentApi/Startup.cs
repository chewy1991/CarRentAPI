using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentApi.Model;
using CarRentApi.Repositories;
using CarRentApi.Repositories.Database;
using CarRentRestAPI.Demo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using zbw.car.rent.api.Repositories.InMemory;

namespace CarRentApi
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
            services.AddControllers();
            services.AddDbContext<CarRentDBContext>(opt => opt.UseInMemoryDatabase("CarRent"));
            if (Configuration.GetValue<string>("UseDbProvider").ToLower() == "entityframework")
            {
                services.AddDbContext<CarRentDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                #region Database Repositories
                services.AddScoped<IRepository<Customer>, DatabaseRepository<Customer>>();
                services.AddScoped<IRepository<Car>, DatabaseRepository<Car>>();
                services.AddScoped<IRepository<CarBrand>, DatabaseRepository<CarBrand>>();
                services.AddScoped<IRepository<CarType>, DatabaseRepository<CarType>>();
                services.AddScoped<IRepository<CarClass>, DatabaseRepository<CarClass>>();
                services.AddScoped<IRepository<Reservation>, DatabaseRepository<Reservation>>();
                services.AddScoped<IRepository<Contract>, DatabaseRepository<Contract>>();
                #endregion
            }
            else
            {
                #region In Memory Repositories
                services.AddSingleton<IRepository<Customer>, InMemoryRepository<Customer>>();
                services.AddSingleton<IRepository<Car>, InMemoryRepository<Car>>();
                services.AddSingleton<IRepository<CarBrand>, InMemoryRepository<CarBrand>>();
                services.AddSingleton<IRepository<CarType>, InMemoryRepository<CarType>>();
                services.AddSingleton<IRepository<CarClass>, InMemoryRepository<CarClass>>();
                services.AddSingleton<IRepository<Reservation>, InMemoryRepository<Reservation>>();
                services.AddSingleton<IRepository<Contract>, InMemoryRepository<Contract>>();
                #endregion
            }
            services.AddSingleton<DemoInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
