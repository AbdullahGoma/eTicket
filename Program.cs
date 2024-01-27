using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace eTickets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var ConStr = builder.Configuration.GetConnectionString("SqlCon");

            //Services Configuration
            
            builder.Services.AddScoped<IActorsService, ActorsService>();




            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Data source =ABDULLAH; Database = eTickets;integrated security = True;TrustServerCertificate=True

            builder.Services.AddDbContext<AppDbContext>(Option => Option.UseSqlServer(ConStr));

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
                pattern: "{controller=Movies}/{action=Index}/{id?}");

            //Seed Database 
            AppDbInitializer.Seed(app);

            app.Run();
        }
    }
}