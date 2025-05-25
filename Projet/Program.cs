using ProjetNet.Bussiness;
using ProjetNet.data;
using ProjetNet.Data;
using ProjetNet.Models;
using ProjetNet.Services;

namespace ProjetNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<InterfaceUtilisateurManager, UtilisateurManager>();
			builder.Services.AddScoped<UtilisateurDAO>();

			builder.Services.AddScoped<InterfaceProjetManager, ProjetManager>();
			builder.Services.AddScoped<ProjetDAO>();

			builder.Services.AddScoped<InterfaceNotificationManager, NotificationManager>();
			builder.Services.AddScoped<NotificationDAO>();

			builder.Services.AddScoped<InterfaceServiceManager, ServiceManager>();
			builder.Services.AddScoped<ServiceDAO>();

			builder.Services.AddScoped<InterfaceDeveloppeurManager, DeveloppeurManager>();
			builder.Services.AddScoped<DeveloppeurDAO>();

			builder.Services.AddScoped<InterfaceTacheManager, TacheManager>();
			builder.Services.AddScoped<TacheDAO>();

			builder.Services.AddAuthentication("Cookies").AddCookie(
				options =>
				{
					options.LoginPath = "/Utilisateur/Signin";
					options.LogoutPath = "/Utilisateur/Signout";
					options.AccessDeniedPath = "/Home/Error";
				}
				);
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});
			var app = builder.Build();

			

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
			app.UseSession();
			app.UseAuthorization();
			app.UseAuthentication();
			app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
