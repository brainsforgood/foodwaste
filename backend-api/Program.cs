
using System;
using Products;
using Repositories;
namespace BackendAPICSharp
{
    class Program {

        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddScoped<IRepository, Repository>();
            var app = builder.Build();

            app.MapControllers();
            app.Run();
        }
    }
}
