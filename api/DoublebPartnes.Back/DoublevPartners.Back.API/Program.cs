using DoublebPartnes.Middleware.MicroORM;

var builder = WebApplication.CreateBuilder(args);
const string corsAllowedProduction = "CORS_ALLOWED_PRODUCTION";
const string corsAllowedDevelopment = "CORS_ALLOWED_DEVELOPMENT";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(configuration =>
{
    configuration.AddPolicy(corsAllowedProduction, options =>
    {
        options
            .AllowCredentials()
            .WithOrigins("https://doublevpartners.com/")
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .AllowAnyHeader();
    });

    configuration.AddPolicy(corsAllowedDevelopment, builder =>
    {
        builder
            .AllowCredentials()
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

var configuration = app.Configuration;

SQLFactory.Configure(configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(corsAllowedDevelopment);
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(corsAllowedProduction);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();