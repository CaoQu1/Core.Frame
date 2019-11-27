using Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Infrastructure
{
    /// <summary>
    /// 数据上下文
    /// </summary>
    public class BaseDbContext : DbContext, IDbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetTypes().Where(t => !t.IsAbstract && typeof(IEntity<>).IsAssignableFrom(t)).Any()).ToList().ForEach(assmbly =>
              {
                  modelBuilder.ApplyConfigurationsFromAssembly(assmbly, x => !x.IsAbstract && !x.IsInterface && typeof(IEntity<>).IsAssignableFrom(x));
              });
            base.OnModelCreating(modelBuilder);
        }
    }
}
