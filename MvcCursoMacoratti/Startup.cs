using LanchesMac.Repositories;
using LanchesMac.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcMacorattiLanchesMac.Areas.Admin.Services;
using MvcMacorattiLanchesMac.Context;
using MvcMacorattiLanchesMac.Models;
using MvcMacorattiLanchesMac.Repositories;
using MvcMacorattiLanchesMac.Repositories.Interfaces;
using ReflectionIT.Mvc.Paging;

namespace LanchesMac;
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
        var connetionString = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>

        options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString),
        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<ILancheRepository, LancheRepository>();
        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        services.AddTransient<IPedidoRepository, PedidoRepository>();

        services.AddScoped<RelatorioVendasService>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin",
                policy => {
                    policy.RequireRole("Admin");
                });
        });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

        services.AddControllersWithViews();

        services.AddPaging(options =>
        {
            options.ViewName = "Bootstrap4";
            options.PageParameterName = "pageindex";
        });

        services.AddMemoryCache();
        services.AddSession();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                          ISeedUserRoleInitial seedUserRoleInitial)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
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


        // criando os cargos
        seedUserRoleInitial.SeedRoles();

        // criando os perfis/users
        seedUserRoleInitial.SeedUsers();

        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
               name: "categoriaFiltro",
               pattern: "Lanche/{action}/{categoria?}",
               defaults: new { Controller = "Lanche", action = "List" });


            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            
        });
    }
}