using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EmployeeAccounting.Db;
using EmployeeAccounting.Db.Core;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Core;
using EmployeeAccounting.Services.Interfaces;
using EmployeeAccounting.UI;
using EmployeeAccounting.UI.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAccounting
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
            services.AddTransient<IContext, Context>();
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, ContextUnitOfWork>();


            services.Scan(scan => scan
                .FromAssemblyOf<ICoreCrud<BaseModel>>()
                .AddClasses(classes => classes.AssignableTo<ICoreService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            //services.AddTransient(typeof(IDepartmentService<>), typeof(DepartmentService<>));
            //services.AddTransient(typeof(IEmployeeService<>), typeof(EmployeeService<>));
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
