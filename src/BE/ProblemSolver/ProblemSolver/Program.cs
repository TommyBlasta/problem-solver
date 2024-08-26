
using ProblemSolver.Application;
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
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(ProblemResolver))!));
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddSingleton<IProblemResolver, ProblemResolver>();

            DependencyInjection.AddInfrastructureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
