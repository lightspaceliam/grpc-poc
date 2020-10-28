using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace GrpcPoc.Entities.Extensions
{
    public static class IWebHostBuilderExtensions
    {
        public static void MigrateDatabase(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureServices(services =>
            {
                var context = services.BuildServiceProvider().GetRequiredService<GrpcPocDbContext>();
                var logger = services.BuildServiceProvider().GetRequiredService<ILogger<IWebHost>>();

                try
                {
                    logger.LogTrace("Applying migrations.");
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occured while applying schema migrations.");
                }
            });
        }
    }
}
