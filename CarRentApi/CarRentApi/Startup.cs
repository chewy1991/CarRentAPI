using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentApi.Controllers.BaseData;
using CarRentApi.Model;
using CarRentApi.Repositories;
using CarRentApi.Repositories.Database;
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
using CarRentApi.ExampleData;

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
            DbContextOptionsBuilder<CarRentDBContext>  builder = new DbContextOptionsBuilder<CarRentDBContext>();
            builder.UseInMemoryDatabase("CarRent");
            DbContextOptions<CarRentDBContext> options = builder.Options;
            
            CarRentDBContext carrent = new CarRentDBContext(options);

            ExampleData.ExampleData.InitTestData(carrent);
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
