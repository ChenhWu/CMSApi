using System;
using Newtonsoft.Json;

namespace CMS_miniAPI.Models
{
	public class BaseOrganization
	{
		public long Id { set; get; }
		public string? Name { set; get; }
		public long? ParentId { set; get; }
		public string? Info { set; get; }

		[JsonIgnore]
		public virtual BaseOrganization? Parent { set; get; }
		[JsonIgnore]
		public virtual ICollection<BaseOrganization>? Children { set; get; }
	}
}

