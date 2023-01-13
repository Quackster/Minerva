using Avatara;
using Avatara.Figure;

namespace Minerva
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            LoadFigureAssets(app.Services.CreateScope());

            app.Run();
        }

        private static void LoadFigureAssets(IServiceScope scope)
        {
            Console.WriteLine("Loading flash assets...");

            FlashExtractor.Instance.Load();

            Console.WriteLine($"{FlashExtractor.Instance.Parts.Count} flash assets loaded");

            Console.WriteLine("Loading figure data...");

            FiguredataReader.Instance.Load();
            Console.WriteLine($"{FiguredataReader.Instance.FigureSets.Count} figure sets loaded");
            Console.WriteLine($"{FiguredataReader.Instance.FigureSetTypes.Count} figure set types loaded");
            Console.WriteLine($"{FiguredataReader.Instance.FigurePalettes.Count} figure palettes loaded");
        }
    }
}