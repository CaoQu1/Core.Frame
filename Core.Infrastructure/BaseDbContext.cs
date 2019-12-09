using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.Repositories;
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
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assmblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("Core") && x.GetTypes().Where(t => t.GetInterfaces().Any(m => m.IsGenericType && m.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).Any()).ToList();
            assmblies.ForEach(assmbly =>
           {
               modelBuilder.ApplyConfigurationsFromAssembly(assmbly, x => x.GetInterfaces().Any(t => t.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
           });
            base.OnModelCreating(modelBuilder);
        }
    }
}
