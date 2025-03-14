using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserService.API.Middleware;
using UserService.Application.Commands.Users.Handlers;
using UserService.Application.Interfaces;
using UserService.Infrastructure.Data;
using UserService.Infrastructure.Messaging;
using UserService.Infrastructure.Repositories;
using UserService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserHandler).Assembly));

builder.Services.AddSingleton<IHostedService, KafkaConsumerService>();

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = builder.Configuration["Kafka:BootstrapServers"],
    GroupId = "user-service-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

builder.Services.AddSingleton<IConsumer<Ignore, string>>(new ConsumerBuilder<Ignore, string>(consumerConfig).Build());

var producerConfig = new ProducerConfig
{
    BootstrapServers = builder.Configuration["Kafka:BootstrapServers"]
};

builder.Services.AddSingleton<IProducer<Null, string>>(new ProducerBuilder<Null, string>(producerConfig).Build());

builder.Services.AddScoped<IUserValidation, UserValidationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserLogRepository, UserLogRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "FinNovaTech API",
        Version = "v1",
        Description = "API para gestionar usuarios y autenticación en FinNovaTech.",
        Contact = new OpenApiContact
        {
            Name = "Gustavo Fernández",
            Email = "soygustavofernandez@gmail.com",
            Url = new Uri("https://github.com/soygustavofernandez")
        },
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserService v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
