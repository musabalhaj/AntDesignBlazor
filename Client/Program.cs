using Append.Blazor.Printing;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Project.Client.Logging;
using Project.Client.Services;
using Project.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Project.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient<IDepartmentVeiwModel, DepartmentVeiwModel>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });

            builder.Services.AddHttpClient<IEmployeeViewModel, EmployeeViewModel>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });

            builder.Services.AddHttpClient<ICategoryService, CategoryService>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });

            builder.Services.AddHttpClient<IArticalService, ArticalService>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });

            builder.Services.AddHttpClient<ILogService, LogService>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });

            builder.Services.AddHttpClient<IPurchasesViewModel, PurchasesViewModel>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });

            builder.Services.AddScoped<IPrintingService, PrintingService>();

            builder.Services.AddSingleton<StatusCodeService>();

            builder.Services.AddScoped(sp =>
            new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

            // custom logging provider
            builder.Services.AddLogging(logging => {
                var httpClient = builder.Services.BuildServiceProvider().GetRequiredService<HttpClient>();
                logging.SetMinimumLevel(LogLevel.Error);
                logging.ClearProviders();
                logging.AddProvider(new ApplicationLoggerProvider(httpClient));
            });

            builder.Services.AddAntDesign();
            await builder.Build().RunAsync();
        }
    }
}
