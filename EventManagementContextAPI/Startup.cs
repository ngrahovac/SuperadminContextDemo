using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementContextAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventManagementAPI", Version = "v1" });
        });

        services.AddDbContext<EventManagementDbContext>((sp, options) => options.UseInMemoryDatabase("eventmanagement-db")
                                                                                .AddInterceptors(new EventManagementDbContextInterceptor(sp.GetService<EventPublishingService>())));

        services.AddScoped<EventHandlingService>();
        services.AddScoped<EventPublishingService>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication2 v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
