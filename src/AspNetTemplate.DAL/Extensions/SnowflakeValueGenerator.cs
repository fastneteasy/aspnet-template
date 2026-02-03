using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Snowflake.Core;

namespace AspNetTemplate.DAL.Extensions;

public class SnowflakeValueGenerator : ValueGenerator<long>
{
    public override long Next(EntityEntry entry)
    {
        var idWorker = entry.Context.GetService<IdWorker>();
        return idWorker.NextId();
    }

    public override bool GeneratesTemporaryValues => false;
}