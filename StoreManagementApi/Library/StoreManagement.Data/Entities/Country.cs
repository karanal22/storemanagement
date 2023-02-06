using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Data.Entities
{
	[Table("Country")]
	public class Country : BaseIdEntity
	{
		[MaxLength(256)]
		public string Name { get; set; }
		public string IsoCode { get; set; }
		public int DisplayOrder { get; set; }
	}
}
