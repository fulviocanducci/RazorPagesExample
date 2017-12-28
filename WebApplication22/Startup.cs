using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication22.Models;

namespace WebApplication22
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer();

            services.AddDbContext<Database>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //configura a página de login
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/admin/login";
            });

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;                
            });

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.RootDirectory = "/Pages";                                       
                    options.Conventions.AddPageRoute("/Test", "peoples/{handler?}");
                    options.Conventions                        
                        .AuthorizeFolder("/admin") // aqui bloquea os arquivos da página sem autenticação
                        .AllowAnonymousToPage("/admin/login"); // aqui libera somente essa página
                });
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");                                
            });                        
        }
    }
}
