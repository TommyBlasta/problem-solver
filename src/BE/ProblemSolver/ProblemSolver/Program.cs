
using FluentValidation;
using ProblemSolver.Application;
using ProblemSolver.Application.Validation;
using ProblemSolver.ErrorHandling;
using ProblemSolver.Infrastructure;
using ProblemSolver.Infrastructure.Persistence;
using System.Reflection;

namespace ProblemSolver
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.UseAllOfForInheritance();
                swaggerGenOptions.UseOneOfForPolymorphism();

                swaggerGenOptions.SelectSubTypesUsing(baseType =>
                    typeof(Program).Assembly.GetTypes().Where(type => type.IsSubclassOf(baseType))
                );
            });

            builder.Services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(ProblemResolver))!);
                c.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddSingleton<IProblemResolver, ProblemResolver>();

            builder.Services.Scan(scan => scan
                .FromAssemblyOf<IProblemResolver>()
                .AddClasses(classes => classes.AssignableTo(typeof(AbstractValidator<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            builder.Services.AddScoped<ErrorHandlingMiddleware>();

            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            DependencyInjection.AddInfrastructureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            await ConfigureApplication(app);

            app.Run();
        }

        private static async Task ConfigureApplication(WebApplication application)
        {
            var dbInitalizer = application.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

            await dbInitalizer.InitialiseAsync();
        }
    }
}
