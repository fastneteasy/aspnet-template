using AspNetTemplate.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Snowflake.Core;

namespace AspNetTemplate.DAL.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddRepository(this IHostApplicationBuilder builder, string connectionStringName)
        {
            var connectionString = builder.Configuration.GetConnectionString(connectionStringName);

            builder.Services.AddDbContext<AspNetTemplateDbContext>(options => options.UseNpgsql(connectionString)
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );

            builder.Services.AddSingleton(serviceProvider => new IdWorker(1, 1));
        }
    }
}
