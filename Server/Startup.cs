using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project.Server.Logging;
using Project.Server.Models;
using System.Linq;

namespace Project.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IArticalRepository, ArticalRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                        builder => {
                            builder.WithOrigins("https://localhost:44345")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                        });
            });
            services.AddControllersWithViews();
            //services.AddControllersWithViews().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //});
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , ILoggerFactory loggerFactory)
        {
            // get the applicationDbContext 
            var serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
            var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            // custom logging provider
            loggerFactory.AddProvider(new ApplicationLoggerProvider(applicationDbContext));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("MyAllowSpecificOrigins");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
