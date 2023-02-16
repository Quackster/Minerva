using Avatara;
using Avatara.Figure;

namespace Minerva
{
    public class Program
    {
        public static bool SHOCKWAVE_BADGE_RENDER = true;
        public static bool FLASH_BADGE_RENDER = false;

        public static void Main(string[] args)
        {
            var argsList = args.ToList();

            if (!argsList.Contains("--shockwave-badge-render") &&
                !argsList.Contains("--flash-badge-render"))
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Warning!");

                Console.ResetColor();
                Console.WriteLine("Needs to be started with either parameter included for finer tuning:");
                Console.WriteLine("--shockwave-badge-render (for Habbo versions using the Shockwave client)");
                Console.WriteLine("--flash-badge-render (for Habbo versions using the Flash client in 2013+)");

                Console.WriteLine();
            }

            if (argsList.Contains("--shockwave-badge-render"))
            {
                Console.WriteLine("Shockwave badge rendering enabled");
                SHOCKWAVE_BADGE_RENDER = true;
            }
            else
            {
                Console.WriteLine("Flash (client versions above and including 2013+) badge rendering enabled");
                FLASH_BADGE_RENDER = false;

            }

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