using MassTransit;
using Microsoft.EntityFrameworkCore;
using project_3.Models;
using project_3.Consumers;

Environment.SetEnvironmentVariable("MT_LICENSE", "true");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DeliveryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHealthChecks();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();
    
    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/");
        
        cfg.ReceiveEndpoint("delivery-orders-queue", e =>
        {
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });
        
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DeliveryDbContext>();
    context.Database.EnsureCreated();
}

app.MapHealthChecks("/health");

app.UseAuthorization();
app.MapControllers();

app.Run();