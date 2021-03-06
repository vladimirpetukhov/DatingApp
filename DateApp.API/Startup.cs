using System;
namespace DateApp.API {

    using System.Net;
    using AutoMapper;
    using Data;
    using Helpers;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<DataContext> (x =>
                x.UseSqlite (Configuration.GetConnectionString ("DefaultConnection")));
            services.AddControllers ().AddNewtonsoftJson (opt => {
                opt.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddCors ();
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme);
            services.AddAutoMapper(typeof(DatingRepository).Assembly);
            services.AddScoped<IAuthRepository, AuthRepository> ();
            services.AddScoped<IDatingRepository, DatingRepository> ();
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_3_0);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
                IServiceProvider serviceProvider = app.ApplicationServices.CreateScope ().ServiceProvider;
                try {
                    var context = serviceProvider.GetService<DataContext> ();
                    Seeder.SeedUsers (context);
                } catch (Exception ex) {
                    throw new InvalidOperationException ("'SEEEEEF'");
                }
            } else {
                app.UseExceptionHandler (builder => {
                    builder.Run (async context => {
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature> ();
                        if (error != null) {
                            context.Response.AddAppError (error.Error.Message);
                            await context.Response.WriteAsync (error.Error.Message);
                        }
                    });
                });
            }

            //app.UseHttpsRedirection();

            app.UseRouting ();

            //app.UseAuthorization();
            app.UseCors (x => x.AllowAnyOrigin ().AllowAnyMethod ().AllowAnyHeader ());
            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });

        }
    }
}