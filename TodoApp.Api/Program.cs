using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Converters;
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

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                });

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

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";

                    var exception = context.Features
                        .Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()
                        ?.Error;

                    if (exception is TodoApp.Application.Validation.DomainRuleException)
                    {
                        await context.Response.WriteAsJsonAsync(new
                        {
                            error = exception.Message
                        });
                    }
                });
            });


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
