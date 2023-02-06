using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Services.Model.Request.StoreProdect
{
	public class PriceFeedModel
	{
		public int StoreId { get; set; }
		public string SKU { get; set; }
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public DateTime Date { get; set; }
	}
}
