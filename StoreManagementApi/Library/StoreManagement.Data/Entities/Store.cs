using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Data.Entities
{
	[Table("Store")]
	public class Store : BaseIdLogEntity
	{
		public string Name { get; set; }
		[ForeignKey("Country")]
		public int CountryId { get; set; }
		[ForeignKey("StateProvince")]
		public int StateProvinceId { get; set; }
		[ForeignKey("City")]
		public int CityId { get; set; }

		public virtual Country Country { get; set; }
		public virtual StateProvince StateProvince { get; set; }
		public virtual City City { get; set; }

	}
}
