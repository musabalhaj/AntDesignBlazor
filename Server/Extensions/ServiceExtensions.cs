using Microsoft.Extensions.DependencyInjection;
using Project.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                        builder => {
                            builder.WithOrigins("https://localhost:44345")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                        });
            });
        }

        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IPurchasesRepository, PurchasesRepository>();
            services.AddScoped<IPurchaseDetailsRepository, PurchaseDetailsRepository>();
        }
    }

}
