using Kompilator.Services;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Kompilator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddMemoryCache();
            builder.Services.AddHttpClient<SimplecastService>(client =>
            { 
                client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("SimplecastBaseURI"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", String.Format("Bearer {0}", builder.Configuration.GetValue<string>("SimplecastToken")));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) 
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}