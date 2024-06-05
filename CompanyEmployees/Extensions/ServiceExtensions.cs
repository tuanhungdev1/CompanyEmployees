using Contracts;
using LoggerService;

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

    }
}
