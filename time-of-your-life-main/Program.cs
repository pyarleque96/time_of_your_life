using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using time_of_your_life.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
{
    builder.Services.AddControllers();

    builder.Services.RegisterDbContext(configuration);

    builder.Services.AddExceptionHandling();

    builder.Services.RegisterAutoMapper();

    builder.Services.RegisterServices();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
{
    if (!app.Environment.IsDevelopment())
    {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();


    app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");

    app.MapFallbackToFile("index.html");

    app.Run();
}

