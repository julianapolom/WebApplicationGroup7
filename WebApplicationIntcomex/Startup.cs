using Business.Intcomex.Class;
using Business.Intcomex.Interfaces;
using DataAcces.Intcomex.Class;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplicationIntcomex.Configuration;

namespace WebApplicationIntcomex
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.Configure<MyConfig>(Configuration.GetSection("MyConfig"));
            var options = new DbContextOptionsBuilder<IntcomexContext>()
                   .UseSqlServer(new SqlConnection(Configuration.GetConnectionString("Connection")))
                   .Options;
            IntcomexContext _context = new(options);
            services.AddScoped<IClientBO, ClientBO>(x => new ClientBO(new UnitOfWork(_context)));
            services.AddScoped<IContractBO, ContractBO>(x => new ContractBO(new UnitOfWork(_context)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Client}/{action=Index}/{id?}");
            });
        }
    }
}
