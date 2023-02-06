using StoreManagement.Common.Model.Request;

namespace StoreManagement.Services.Model.Request.Base
{
	public class BaseListRequest : PaginationRequest
	{
		public string SearchText { get; set; }

		public bool IsActive { get; set; }
	}
}