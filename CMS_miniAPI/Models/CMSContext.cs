using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace CMS_miniAPI.Models
{
	public class CMSContext:DbContext
	{
		// 配置 数据库提供程序 相关设置
		public CMSContext(DbContextOptions<CMSContext>options):base(options)
		{
		}

		public DbSet<BaseBeacon> Beacons { set; get; } = null!;
		public DbSet<BaseCar> Cars { set; get; } = null!;
		public DbSet<BaseOrganization> Organizations { set; get; } = null!;
		public DbSet<BeaconCarRelation> BeaconCarRelations { set; get; } = null!;
		public DbSet<CarOrganizationRelation> CarOrganizationRelations { set; get; } = null!;

        // Context 阶段 钩子函数
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
			// 配置数据库提供程序
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			// 配置实体模型
			// 1. Orgnization 树形结构
			modelBuilder.Entity<BaseOrganization>()
				.HasOne(x => x.Parent)
				.WithMany(x=>x.Children)
				.HasForeignKey(x => x.ParentId)
				.OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}

