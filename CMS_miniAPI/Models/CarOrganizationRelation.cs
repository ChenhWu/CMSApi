using System;
namespace CMS_miniAPI.Models
{
	public class CarOrganizationRelation
	{
		public long Id { set; get; }
		public long? BaseCarId { set; get; }
		public long? BaseOrganizationId { set; get; }
		public string? Info { set; get; }
	}
}

