using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Common.ExceptionHandler;
using StoreManagement.Common.Helper;
using StoreManagement.Common.Model.Response;
using StoreManagement.Data.Context;
using StoreManagement.Data.Entities;
using StoreManagement.Data.Helper;
using StoreManagement.Data.Repository;
using StoreManagement.Services.Model.Request.StoreProdect;
using StoreManagement.Services.Model.Response.StoreProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreManagement.Services.Service.StoreProduct
{
	public class StoreProductService : IStoreProductService
	{
		#region Properties
		public IRepository<Data.Entities.StoreProduct> _storeProdctRepository { get; }
		public IRepository<Data.Entities.Product> _prodctRepository { get; }
		public IHttpContextAccessor _httpContextAccessor { get; }
		public ApplicationDbContext _dbContext { get; }
		public IMapper _mapper { get; }
		#endregion
		#region "Constructor"
		public StoreProductService(IHttpContextAccessor httpContextAccessor,
			ApplicationDbContext dbContext,
			IMapper mapper,
			IRepository<Data.Entities.StoreProduct> storeProdctRepository,
			IRepository<Data.Entities.Product> prodctRepository)
		{
			_storeProdctRepository = storeProdctRepository;
			_prodctRepository = prodctRepository;
			_httpContextAccessor = httpContextAccessor;
			_dbContext = dbContext;
			_mapper = mapper;
		}
		#endregion

		#region "Interface Methods"
		public async Task UploadPriceFeed(string csvContent)
		{
			List<PriceFeedModel> priceFeeds = CsvUtil.ReadCSVString(csvContent, CsvMapper);

			List<Data.Entities.StoreProduct> storeProducts = new List<Data.Entities.StoreProduct>();

			foreach (PriceFeedModel priceFeed in priceFeeds)
			{
				var product = await GetProductAndCreateIfNotExist(priceFeed.ProductName);

				Data.Entities.StoreProduct storeProduct = _mapper.Map<Data.Entities.StoreProduct>(priceFeed);
				storeProduct.ProductId = product.Id;

				storeProducts.Add(storeProduct);
			}

			await _storeProdctRepository.AddRangeAsync(storeProducts);
		}

		public async Task<PaginationResponse<StoreProductModel>> List(GetStoreProductList request)
		{
			var list = new List<Expression<Func<Data.Entities.StoreProduct, bool>>> { };

			if (!string.IsNullOrEmpty(request.ProductName))
			{
				list.Add(x => x.Product.Name.Contains(request.ProductName));
			}

			if (!string.IsNullOrEmpty(request.SKU))
			{
				list.Add(x => x.SKU.Contains(request.SKU));
			}

			if (request.StoreId.HasValue)
			{
				list.Add(x => x.StoreId == request.StoreId);
			}

			var query = _storeProdctRepository.TableNoTracking.Include(x => x.Product).Include(x => x.Store).ThenInclude(x => x.City).AsQueryable();
			list.ForEach(filter => { query = query.Where(filter); });
			query = query.CustomOrderBy(request.SortBy);
			var entities = query.Skip(request.GetSkip()).Take(request.GetTake()).ToList().Select(x => PrepareStoreProductResponse(x)).ToList();

			var count = _storeProdctRepository.Count(list);
			return new PaginationResponse<StoreProductModel>(entities, count, request.PageNumber, request.PageSize);
		}

		public async Task<StoreProductModel> GetById(int id)
		{
			var storeProduct = await Read(id);
			return PrepareStoreProductResponse(storeProduct);
		}

		public async Task<StoreProductModel> Update(UpdateStoreProduct request)
		{
			var storeProduct = await Read(request.Id);
			storeProduct = _mapper.Map(request, storeProduct);
			await _storeProdctRepository.UpdateAsync(storeProduct);

			return PrepareStoreProductResponse(storeProduct);
		}


		#endregion


		#region "Private Methods"

		private StoreProductModel PrepareStoreProductResponse(Data.Entities.StoreProduct StoreProdct)
		{
			var response = _mapper.Map<StoreProductModel>(StoreProdct);
			return response;
		}

		private async Task<Data.Entities.StoreProduct> Read(int id, bool defaultNull = false)
		{
			var hotel = await _storeProdctRepository.GetByIdAsync(id);
			if ((hotel == null) && !defaultNull)
				throw new AppException(AppErrorCode.NotFoundStoreProdct, new[] { id.ToString() });

			return hotel;
		}

		private PriceFeedModel CsvMapper(string csvLine)
		{
			string[] values = csvLine.Split(',');

			PriceFeedModel folderClientMappingModel = new PriceFeedModel()
			{
				StoreId = Convert.ToInt32(values[0]),
				SKU = values[1],
				ProductName = values[2],
				Price = Convert.ToDecimal(values[3]),
				Date = Convert.ToDateTime(values[4])
			};
			return folderClientMappingModel;
		}


		private async Task<Data.Entities.Product> GetProductAndCreateIfNotExist(string productName)
		{
			var product = await _prodctRepository.FirstOrDefaultAsync(x => x.Name == productName);

			if (product == null)
			{
				product = new Product()
				{
					Name = productName
				};
				await _prodctRepository.InsertAsync(product);
			}

			return product;
		}

		#endregion
	}
}
