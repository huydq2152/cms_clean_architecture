using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Infrastructure.Extensions;
using CleanArchitecture.Persistence.Contexts.Seed;
using CleanArchitecture.Persistence.Extensions;
using CleanArchitecture.WebAPI.Extensions;
using Infrastructure.Logging;
using Infrastructure.ScheduledJobs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information($"Start {builder.Environment.ApplicationName} up");

try
{
    var blogCorsPolicy = "BlogCorsPolicy";

    builder.Host.AddAppConfigurations();
    
    // Add services to the container.
    builder.Services.AddApplicationLayer();
    builder.Services.AddInfrastructureLayer(builder.Configuration);
    builder.Services.AddPersistenceLayer(builder.Configuration);
    builder.Services.AddWebApiLayer(builder.Configuration);
    builder.Services.AddCorsPolicy(builder.Configuration, blogCorsPolicy);

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

    app.UseMiddleware<ErrorWrappingMiddleware>();
    app.UseStaticFiles();
    app.UseCors(blogCorsPolicy);
    app.UseRouting();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.UseHangfireDashboard(builder.Services);
    
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

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