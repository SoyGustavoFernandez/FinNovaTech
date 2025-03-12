using Microsoft.EntityFrameworkCore;
using UserService.Application.Handlers.Users;
using UserService.Application.Interfaces;
using UserService.Infrastructure.Data;
using UserService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserHandler).Assembly));

builder.Services.AddScoped<IUserValidation, UserValidationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
