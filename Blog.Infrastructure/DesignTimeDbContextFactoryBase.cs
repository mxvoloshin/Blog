using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Blog.Infrastructure
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext: DbContext
    {
        private const string ConnectionStringName = "BlogDB";
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        public TContext CreateDbContext(string[] args)
        {
            Console.WriteLine($"DesignTimeDbContextFactoryBase'.");

            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}Blog.WebApi", Path.DirectorySeparatorChar);
            return Create(basePath, Environment.GetEnvironmentVariable(AspNetCoreEnvironment));
        }

        private TContext Create(string basePath, string environmentName)
        {
            Console.WriteLine($"{basePath} - {environmentName}");

            var configuration = new ConfigurationBuilder()
                                .SetBasePath(basePath)
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                                .AddEnvironmentVariables()
                                .Build();

            var connectionString = configuration.GetConnectionString(ConnectionStringName);
            return Create(connectionString);
        }

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}