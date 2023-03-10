using StoreManagement.Common.ExceptionHandler;
using StoreManagement.Services.Model.Request.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Services.Model.Request.StoreProdect
{
	public class UpdateStoreProduct
	{
		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessageResource))]
		public int Id { get; set; }
		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessageResource))]
		public int StoreId { get; set; }
		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessageResource))]
		public string SKU { get; set; }
		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessageResource))]
		public decimal Price { get; set; }
		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessageResource))]
		public DateTime Date { get; set; }
	}
}
