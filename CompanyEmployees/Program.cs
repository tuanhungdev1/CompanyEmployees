using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);


// Đường dẫn tới tệp cấu hình
var configFilePath = Path.Combine(AppContext.BaseDirectory, "./nlog.config.txt");

// Kiểm tra xem tệp cấu hình có tồn tại không
if (!File.Exists(configFilePath)) {
    throw new FileNotFoundException($"NLog configuration file not found: {configFilePath}");
}

// Tải cấu hình NLog bằng phương thức mới
LogManager.Setup().LoadConfigurationFromFile(configFilePath);


builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
} else {
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions {
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
