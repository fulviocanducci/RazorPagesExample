using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication22.Models;
//https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?tabs=visual-studio%2Caspnetcore2x
//https://medium.com/@renato.groffe/17-exemplos-de-projetos-criados-com-o-asp-net-core-2-0-99e71002a65
//http://www.hishambinateya.com/authentication-and-authorization-in-razorpages
// https://www.youtube.com/watch?v=6LzmEOvzt1A 31:45
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

            services                
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()                           
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/admin/login";
                config.AccessDeniedPath = "/admin/login";
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
                        .AllowAnonymousToPage("/admin/login") // aqui libera somente essa página
                        .AuthorizeFolder("/users", "AdminPolicy");
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                {
                    policy.Requirements.Add(new AdminRequirement());
                });
            });

            services.AddSingleton<IAuthorizationHandler, HasAdminRole>();
                        
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
