
using System;
using Models;
using Repositories;
namespace BackendAPICSharp
{
    class Program {

        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            var app = builder.Build();

            app.MapControllers();
            app.Run();
        }
    }
}
