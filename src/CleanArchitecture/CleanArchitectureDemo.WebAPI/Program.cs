using CleanArchitectureDemo.Application.Extensions;
using CleanArchitectureDemo.Infrastructure.Extensions;
using CleanArchitectureDemo.Infrastructure.Persistence.Contexts;
using Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information($"Start {builder.Environment.ApplicationName} up");

try
{
    // Add services to the container.
    builder.Services.AddApplicationLayer();
    builder.Services.AddInfrastructureLayer(builder.Configuration);
    builder.Services.AddControllers();
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    var app = builder.Build();

    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
    }
    
    using (var scope = app.Services.CreateScope())
    {
        var applicationContextSeed = scope.ServiceProvider.GetRequiredService<ApplicationContextSeed>();
        await applicationContextSeed.InitialiseAsync();
        await applicationContextSeed.SeedAsync();
    }

    // app.UseHttpsRedirection();
    // app.UseAuthorization();
    app.MapDefaultControllerRoute();

    app.Run();
}
catch (Exception e)
{
    var type = e.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(e, "Unhandled exception");
}
finally
{
    Log.Information("Shutdown success");
    Log.CloseAndFlush();
}