using FluentValidation;
using FluentValidation.AspNetCore;
using Htmx.TagHelpers;
using HTMXTest.Services;
using HTMXTest.Validator;

namespace HTMXTest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<PersonModelValidator>();
            builder.Services.AddSingleton<IViewLocatorService, ViewLocatorService>();

            var app = builder.Build();

            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == 404)
                {
                    response.Redirect("/Error/404");
                }
                else if (response.StatusCode == 500)
                {
                    response.Redirect("/Error/500");
                }
            });
            app.UseExceptionHandler("/Error/500");
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapHtmxAntiforgeryScript();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
