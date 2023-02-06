using StoreManagement.Common.Model.Response;
using StoreManagement.Services.Model.Request.StoreProdect;
using StoreManagement.Services.Model.Response.StoreProduct;
using System.Threading.Tasks;

namespace StoreManagement.Services.Service.StoreProduct
{
	public interface IStoreProductService
	{
		public Task UploadPriceFeed(string csvContent);
		public Task<PaginationResponse<StoreProductModel>> List(GetStoreProductList request);
		public Task<StoreProductModel> GetById(int id);
		public Task<StoreProductModel> Update(UpdateStoreProduct request);
	}
}
