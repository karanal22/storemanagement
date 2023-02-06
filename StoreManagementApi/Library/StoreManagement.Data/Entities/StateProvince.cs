using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Data.Entities
{
	[Table("StateProvince")]
	public class StateProvince : BaseIdLogEntity
	{
		[ForeignKey("Country")]
		public int CountryId { get; set; }

		[MaxLength(256)]
		public string Name { get; set; }
		public string Abbreviation { get; set; }
		public int DisplayOrder { get; set; }
		public virtual Country Country { get; set; }
	}
}
