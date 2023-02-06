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
	[Table("StoreProduct")]
	public class StoreProduct : BaseIdLogEntity
	{
		[ForeignKey("Store")]
		public int StoreId { get; set; }
		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public string SKU { get; set; }
		public decimal Price { get; set; }
		public DateTime Date { get; set; }

		public virtual Store Store { get; set; }
		public virtual Product Product { get; set; }
	}
}
