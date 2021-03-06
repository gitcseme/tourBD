using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using tourBD.Membership.Contexts;
using tourBD.Membership.Entities;
using tourBD.Membership.Services;
using tourBD.Membership;
using tourBD.Forum.Contexts;
using tourBD.Forum;
using tourBD.Membership.Seeds;
using tourBD.Forum.Seeds;
using Autofac.Core;
using Microsoft.CodeAnalysis.Options;
using Microsoft.AspNetCore.Authentication;
using tourBD.NotificationChannel.Contexts;
using tourBD.NotificationChannel;
using tourBD.NotificationChannel.Seeds;

namespace tourBD.Web
{
    public class Startup
    {
        public string connectionStringName = "DefaultConnection";
        public string connectionString;
        public string migrationAssemblyName;

        public Startup(IWebHostEnvironment env)
        {
            // In ASP.NET Core 3.0 `env` will be an IWebHostEnvironment, not IHostingEnvironment.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();

            connectionString = Configuration.GetConnectionString(connectionStringName);
            migrationAssemblyName = typeof(Startup).Assembly.FullName;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationAssemblyName))
            );

            services.AddDbContext<ForumContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationAssemblyName))
            );

            services.AddDbContext<NotificationContext>(option =>
                option.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationAssemblyName))
            );

            services
                .AddIdentity<ApplicationUser, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<UserManager>()
                .AddRoleManager<RoleManager>()
                .AddSignInManager<SignInManager>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddRazorPages();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 6;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration["Authentication:Google:AppId"];
                    options.ClientSecret = Configuration["Authentication:Google:AppSecret"];
                    options.ClaimActions.MapJsonKey("urn:google:image", "picture");
                })
                .AddFacebook(options => 
                {
                    options.ClientId = Configuration["Authentication:Facebook:AppId"];
                    options.ClientSecret = Configuration["Authentication:Facebook:AppSecret"];
                });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterModule(new MembershipModule(connectionString, migrationAssemblyName));
            builder.RegisterModule(new ForumModule(connectionString, migrationAssemblyName));
            builder.RegisterModule(new NotificationModule(connectionString, migrationAssemblyName));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            AuthoritySeed authoritySeed,
            ForumSeed forumSeed,
            NotificationSeed notificationSeed)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            // Automating migration and seeding data
            authoritySeed.MigrateAsync().Wait();
            authoritySeed.SeedAsync().Wait();

            forumSeed.MigrateAsync().Wait();

            notificationSeed.MigrateAsync().Wait();
        }
    }
}
