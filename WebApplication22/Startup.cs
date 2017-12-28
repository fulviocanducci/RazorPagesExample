using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebApplication22.Models;

namespace WebApplication22
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer();

            services.AddDbContext<Database>();

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;                
            });

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.RootDirectory = "/Pages";                                       
                    options.Conventions.AddPageRoute("/Test", "peoples/{handler?}");
                });
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");                
                
            });
                        
        }
    }
}
