using Contracts;
using LoggerService;
using Service.Contracts;
using Service;
using Repository;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployees.Extensions {
    public static class ServiceExtensions {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options => {

                
                //Có thể sử dụng các hàm khác như WithOrigins("https://example.com") tường tự với WithMethods("POST", "GET") để chi cho phép các method
                // Cũng tương tự với WithHeaders("accept", ""content-type)
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options => {

            });

        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddSingleton<ILoggerManager, LoggerManager>();


        public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();


        public static void ConfigureServiceManager(this IServiceCollection services) =>
    services.AddScoped<IServiceManager, ServiceManager>();


        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

    }
}
