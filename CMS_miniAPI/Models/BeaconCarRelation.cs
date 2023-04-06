using System;
namespace CMS_miniAPI.Models
{
	public class BeaconCarRelation
	{
		public long Id { set; get; }
		public long? BaseCarId { set; get; }
		public long? BaseBeaconId { set; get; }
		public string? Info { set; get; }
	}
}

