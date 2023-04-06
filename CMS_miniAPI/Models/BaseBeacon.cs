using System;
namespace CMS_miniAPI.Models
{
	public class BaseBeacon
	{
		public long Id { set; get; }
		public double? PosX { set; get; }
		public double? PosY { set; get; }
		public string? Info { set; get; }
	}
}

