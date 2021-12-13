using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using TestCI.Common.Builder;
using TestCI.DAL;
using TestCI.DAL.Builder;
using TestCI.HostedService.Builder;
using TestCI.Services.Builder;

namespace TestCI.Web
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
               .AddJsonOptions(options =>
               {
                   
               });
            services.AddCors(options =>
            {
                options.AddPolicy("All", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "test CI API",
                    Version = "1.0",
                });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "TestCI.xml");

                if (File.Exists(filePath))
                {
                    options.IncludeXmlComments(filePath);
                }

                options.CustomSchemaIds(x => x.FullName);
            });

            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("database"));
            services.AddRepositories();
            services.AddTimer();
            services.AddTaskManager();
            services.AddServices();
            services.AddHostedServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            });
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(pr =>
            {
                pr.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "Test API";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
                options.DefaultModelsExpandDepth(-1);
                options.InjectStylesheet("/swagger-theme.css");
            });
        }
    }
}
