using System;
using Microsoft.EntityFrameworkCore;

namespace CMS_miniAPI.Models
{
	public class TestContext:DbContext
	{
		public TestContext(DbContextOptions<TestContext> options) : base(options)
		{
		}

		public DbSet<TestItem> TestItems { set; get; } = null!;
	}
}

