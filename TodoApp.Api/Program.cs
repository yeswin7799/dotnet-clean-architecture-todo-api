using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Application.Validation;
using TodoApp.Infrastructure.Persistence;
using TodoApp.Infrastructure.Repositories;
using TodoApp.Infrastructure.Services;

namespace TodoApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // -------------------------------
            // Add services to the container
            // -------------------------------

            builder.Services.AddControllers();

            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // -------------------------------
            // EF Core - InMemory Database
            // -------------------------------
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TodoAppDb"));

            // -------------------------------
            // Dependency Injection
            // -------------------------------

            // Repository
            builder.Services.AddScoped<ITaskRepository, EfTaskRepository>();

            // Application services
            builder.Services.AddScoped<TaskService>();
            builder.Services.AddScoped<TaskRules>();

            // Infrastructure services
            builder.Services.AddSingleton<IHolidayService, HolidayService>();
            builder.Services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

            var app = builder.Build();

            // -------------------------------
            // Configure the HTTP request pipeline
            // -------------------------------
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
