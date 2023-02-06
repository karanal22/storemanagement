using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Data.Entities
{
	[Table("City")]
	public class City : BaseIdEntity
	{
		[ForeignKey("StateProvince")]
		public int StateProvinceId { get; set; }
		[MaxLength(256)]
		public string Name { get; set; }
		public string Abbreviation { get; set; }
		public int DisplayOrder { get; set; }

		public virtual StateProvince StateProvince { get; set; }
	}
}
