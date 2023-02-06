using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Services.Model.Response.StoreProduct
{
	public class StoreProductModel
	{
		public int Id { get; set; }
		public string StoreId { get; set; }
		public string StoreName { get; set; }
		public string StoreCity { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string SKU { get; set; }
		public DateTime Date { get; set; }
		public decimal Price { get; set; }
	}
}
