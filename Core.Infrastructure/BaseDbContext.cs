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
        /// <summary>
        /// 初始化数据连接并创建数据库
        /// </summary>
        /// <param name="options"></param>
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// 创建数据模型
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assmblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("Core") && x.GetTypes().Where(t => t.GetInterfaces().Any(m => m.IsGenericType && m.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).Any()).ToList();
            assmblies.ForEach(assmbly =>
           {
               modelBuilder.ApplyConfigurationsFromAssembly(assmbly, x => x.GetInterfaces().Any(t => t.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
           });
            //InitData();
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {

            var roleEntry = Entry<Role>(new Role
            {
                RoleName = "系统管理员",
                RoleDesc = "超级权限",
                Status = Global.CoreEnum.Status.Enable,
                CreateTime = DateTime.Now,
                SortId = 1,
                SystemId = 1
            });
            roleEntry.State = EntityState.Added;

            var userEntry = Entry<SystemUser>(new SystemUser
            {
                CreateTime = DateTime.Now,
                Mobile = "15196613744",
                NickName = "曹瞿",
                RegTime = DateTime.Now,
                PassWord = "698d51a19d8a121ce581499d7b701668",
                UserName = "cq",
                SystemId = 1,
                Sex = Global.CoreEnum.Sex.Man,
                HeadPortrait = ""
            });
            userEntry.State = EntityState.Added;

            var userRoleEntry = Entry<SystemUserRole>(new SystemUserRole
            {
                RoleId = roleEntry.Entity.Id,
                SortId = 1,
                SystemId = 1,
                CreateTime = DateTime.Now,
                SystemUserId = userEntry.Entity.Id
            });
            userRoleEntry.State = EntityState.Added;

            var actionEntry = Entry<ActionPermissions>(new ActionPermissions
            {
                ActionName = "首页",
                CreateTime = DateTime.Now,
                Icon = "layui-icon-file-b",
                ShowOrder = 1,
                SortId = 1,
                Action = "Index",
                Type = Global.CoreEnum.Operation.List
            });
            actionEntry.State = EntityState.Added;

            var controllerEntry = Entry<ControllerPermissions>(new ControllerPermissions
            {
                ModuleName = "主页",
                CreateTime = DateTime.Now,
                Area = "Admin",
                Controller = "Home",
                ModuleUrl = "Admin/Home/Index",
                Icon = "layui-icon-home",
                IsShow = true,
                ShowOrder = 1,
                SortId = 1
            });
            controllerEntry.State = EntityState.Added;

            var controllerActionEntry = Entry<ControllerActionPermissions>(new ControllerActionPermissions
            {
                CreateTime = DateTime.Now,
                SortId = 1,
                ActionId = actionEntry.Entity.Id,
                SystemId = 1,
                ControllerId = controllerEntry.Entity.Id
            });
            controllerActionEntry.State = EntityState.Added;

            var controllerActionRoleEntry = Entry<ContollerActionRole>(new ContollerActionRole
            {
                RoleId = roleEntry.Entity.Id,
                SortId = 1,
                SystemId = 1,
                CreateTime = DateTime.Now,
                ControllerActionId = controllerActionEntry.Entity.Id
            });
            controllerActionRoleEntry.State = EntityState.Added;

            SaveChanges();
        }
    }
}
