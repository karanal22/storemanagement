using StoreManagement.Services.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Services.Model.Request.StoreProdect
{
	public class GetStoreProductList : BaseListRequest
	{
		public int? StoreId { get; set; }

		public string ProductName { get; set; }

		public string SKU { get; set; }
	}
}
