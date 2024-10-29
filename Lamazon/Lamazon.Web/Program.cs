using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Implementations;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Services.Implemetations;
using Lamazon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lamazon.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            //Add DbContext
            builder.Services.AddDbContext<LamazonDbContext>(options =>
            {
                options.UseSqlServer("Server=MICA\\SQLEXPRESS;Database=LamazonDb;Trusted_Connection=True;TrustServerCertificate=true");
            });

            //Add Repositories
            builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();


            //AddServices
            builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
