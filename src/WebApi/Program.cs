using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using WebApi.Middleware;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddPersistence();

            builder.Services.AddRepositories();

            builder.Services.AddCommandQueryServices();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            //app.UseHttpsRedirection();

            using(var scope = app.Services.CreateScope())
            {
                var writeContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
                writeContext.Database.OpenConnection();
                writeContext.Database.EnsureCreated();
                WriteDbContext.SeedData(writeContext);
            }

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
