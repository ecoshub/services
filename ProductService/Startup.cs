using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductService.Contexts;
using ProductService.Repositories;

namespace ProductService {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<ProductDatabaseContext> (options =>
                options.UseNpgsql (
                    Configuration.GetConnectionString ("DB_CONNECTION_STRING")
                )
            );
            services.AddControllers ();
            services.AddAutoMapper (AppDomain.CurrentDomain.GetAssemblies ());
            services.AddScoped<IProductRepository, ProductRepository> ();
        }

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env, ProductDatabaseContext context) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            app.UseRouting ();
            app.UseAuthorization ();
            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });

            context.Database.Migrate ();

            // Database seeding code block.
            // try {
            //     DatabaseSeed.Seeding.Seed (context);
            // } catch (DbUpdateException) {
            //     Console.WriteLine ("Dataseed already exists.");
            // } catch (Exception) {
            //     Console.WriteLine ("Dataseeding problem!");
            // }
        }
    }
}
