using Microsoft.EntityFrameworkCore;
using Promptlet.Infrastructure;
using Promptlet.Infrastructure.Data;

internal class Program
{
    static async Task Main(string[] args)
    {
        using (var context = new PromptletDbContextFactory().CreateDbContext())
        {
            DbInit.Init(context);
            var releases = await context.PromptletCollections.Take(5).ToListAsync();

            foreach (var release in releases)
            {
                Console.WriteLine(release.PromptletCollectionName);
            }
        }

        Console.WriteLine("\r\nPress any key to continue ...");
        Console.Read();
    }
}