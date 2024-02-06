using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Infrastructure.Extensions;
using CleanArchitecture.Persistence.Contexts;
using CleanArchitecture.Persistence.Contexts.Seed;
using CleanArchitecture.Persistence.Extensions;
using CleanArchitecture.WebAPI.Extensions;
using Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information($"Start {builder.Environment.ApplicationName} up");

try
{
    builder.Host.AddAppConfigurations();
    // Add services to the container.
    builder.Services.AddApplicationLayer(builder.Configuration);
    builder.Services.AddInfrastructureLayer();
    builder.Services.AddPersistenceLayer(builder.Configuration);
    builder.Services.AddWebApiLayer();
    
    var app = builder.Build();

    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("AdminAPI/swagger.json", "Amin API");
            options.DisplayOperationId();
            options.DisplayRequestDuration();
        });
        app.UseDeveloperExceptionPage();
    }
    // else
    // {
    //     app.UseExceptionHandler("/Home/Error");
    //     app.UseHsts();
    // }
    
    using (var scope = app.Services.CreateScope())
    {
        var applicationContextSeed = scope.ServiceProvider.GetRequiredService<ApplicationContextSeed>();
        await applicationContextSeed.InitialiseAsync();
        await applicationContextSeed.SeedAsync();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication(); 
    app.UseAuthorization();
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