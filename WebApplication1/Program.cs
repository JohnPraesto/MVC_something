using CouponAPI.Repositories;
using WebApplication1.Controllers;
using WebApplication1.Services;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.     
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<ICouponService, CouponService>();

            //LÄGGER DITT MANUELL ADD SERVICES
            builder.Services.AddScoped<ICouponService, CouponService>();
            builder.Services.AddHttpClient();

            StaticDetails.CouponApiBase = builder.Configuration["ServiceUrls:SUT23CouponAPI"]; // https://localhost:7044/ // måste va exakt samma som i appsettings

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
