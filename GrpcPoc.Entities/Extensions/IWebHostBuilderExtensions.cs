using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Linq;

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

        public static void SeedDatabase(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureServices(services => {

                var context = services.BuildServiceProvider().GetRequiredService<GrpcPocDbContext>();
                var logger = services.BuildServiceProvider().GetRequiredService<ILogger<IWebHost>>();
                var culture = CultureInfo.CreateSpecificCulture("en-AU");

                var people = context.People
                    .ToList();

                if (people.Any())
                {
                    logger.LogInformation("DELETE all People");

                    context.People.RemoveRange(people);
                    context.SaveChanges();

                    /**
                     * Does not reseed identity. Execute the following if required:
                     * DELETE FROM People;
                     * DBCC CHECKIDENT ('People', RESEED, 0);
                     */
                }

                logger.LogInformation("Seed People");

                var utcNow = DateTime.UtcNow;

                context.People.AddRange(new Person[]
                {
                    new Person { FirstName = "Homer", MiddleName = "J", LastName = "Simpson", DateOfBirth = DateTime.Parse("1956-05-12", culture), Created = utcNow, LastModified = utcNow },
                    new Person { FirstName = "Bart", MiddleName = default(string), LastName = "Simpson", DateOfBirth = DateTime.Parse("1980-04-01", culture), Created = utcNow, LastModified = utcNow },
                    new Person { FirstName = "Milhouse", MiddleName = "Van", LastName = "Houten", DateOfBirth = DateTime.Parse("1980-07-01", culture), Created = utcNow, LastModified = utcNow },
                });

                context.SaveChanges();
            });
            
        }
    }
}
