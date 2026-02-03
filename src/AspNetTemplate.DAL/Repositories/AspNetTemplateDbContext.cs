using AspNetTemplate.DAL.Entities;
using AspNetTemplate.DAL.Extensions;
using AspNetTemplate.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Snowflake.Core;
using System.Linq.Expressions;

namespace AspNetTemplate.DAL.Repositories;

public class AspNetTemplateDbContext : DbContext
{
    public AspNetTemplateDbContext(DbContextOptions<AspNetTemplateDbContext> options, IdWorker idWorker) : base(options)
    {
        //https://stackoverflow.com/questions/69961449/net6-and-datetime-problem-cannot-write-datetime-with-kind-utc-to-postgresql-ty
        //https://github.com/npgsql/doc/pull/116
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> Users { get; init; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasQueryFilter(x => x.deleted_at == null)
            .Property(b => b.id)
            .HasValueGenerator<SnowflakeValueGenerator>();
    }
}

public static class DbSetExtension
{
    public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int size)
    {
        return source.Skip((page - 1) * size).Take(size);
    }

    public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int size)
    {
        return source.Skip((page - 1) * size).Take(size);
    }

    public static async Task<PageCollection<TModel>> ToPageAsync<TSource, TModel>(this IQueryable<TSource> source,
        PageParameter parameter, Func<TSource, TModel> map) where TSource : class where TModel : class
    {
        var count = await source.LongCountAsync();
        if (parameter.Pagination)
        {
            source = source.Page(parameter.PageNumber, parameter.PageSize);
        }

        var collections = await source.AsNoTracking().ToListAsync();
        return new PageCollection<TModel>(count, parameter.PageSize, collections.Select(map).ToList());
    }

    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}