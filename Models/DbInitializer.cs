using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace JsonPatchSample.Models
{
    public class DbInitializer
    {
        public static void Initialize(BloggingContext context, IServiceProvider services)
        {
            var logger = services.GetRequiredService<ILogger<DbInitializer>>();

            context.Database.EnsureCreated();

            logger.LogInformation("Start seeding the database.");

            if (!context.Blogs.Any())
            {
                context.Blogs.Add(new Blog { BlogId = 1, Url = "xcode.me" });
                context.SaveChanges();
            }

            if (!context.Posts.Any())
            {
                context.Posts.Add(new Post { BlogId = 1, PostId = 1, Title = "First post", Content = "Test 1" });
                context.Posts.Add(new Post { BlogId = 1, PostId = 2, Title = "Second post", Content = "Test 2" });
                context.SaveChanges();
            }

            logger.LogInformation("Finished seeding the database.");
        }
    }
}
