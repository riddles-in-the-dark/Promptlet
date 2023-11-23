using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Promptlet.Infrastructure.Data;

namespace Promptlet.Infrastructure
{
    public class PromptletDbContextFactory : IDesignTimeDbContextFactory<PromptletDbContext>
    {
        private static string _connectionString;

        public PromptletDbContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public PromptletDbContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<PromptletDbContext>();
            builder.UseSqlServer(_connectionString);

            return new PromptletDbContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
