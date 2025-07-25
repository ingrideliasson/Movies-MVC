using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesMVC.Models;
using MoviesMVC.Data;

namespace MoviesMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // add connection string
            builder.Services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MovieContext") ?? throw new InvalidOperationException("Connection string 'MovieContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Seed data at startup (ChatGPT helped me with the scope logic)
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MovieContext>();
                DbInitializer.Initialize(context);
            }

            // Security and UI things
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
        
            app.UseRouting(); 

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movies}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

