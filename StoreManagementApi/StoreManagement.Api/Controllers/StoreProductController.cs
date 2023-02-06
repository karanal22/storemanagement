using Microsoft.AspNetCore.Mvc;
using StoreManagement.Common.ExceptionHandler;
using StoreManagement.Common.Model.Response;
using StoreManagement.Services.Model.Request.StoreProdect;
using StoreManagement.Services.Model.Response.StoreProduct;
using StoreManagement.Services.Service.StoreProduct;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace StoreManagement.Api.Controllers
{
	[Route("api/v1/StoreProdct")]
	[ApiController]
	public class StoreProductController : BaseController
	{
		private readonly IStoreProductService _storeProductService;

		public StoreProductController(IServiceProvider serviceProvider,
			IStoreProductService storeProductService) : base(serviceProvider)
		{
			_storeProductService = storeProductService;
		}



		[HttpPost("UploadPriceFeed")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult> UploadPriceFeed()
		{
			if (Request.Form.Files.Count == 0)
				return BadRequest("Please upload at least one file");

			var form = Request.Form;

			if (form.Files.Count == 0)
				throw new AppException(AppErrorCode.NoFileUploaded);

			// check empty file
			var file = form.Files[0];

			if (file == null || file.Length == 0)
				throw new AppException(AppErrorCode.EmptyFIle);

			string ext = Path.GetExtension(file.FileName);
			if (ext != ".csv")
				throw new AppException(AppErrorCode.InvalidFileType);

			using (StreamReader inputStreamReader = new StreamReader(file.OpenReadStream()))
			{
				var csvContent = inputStreamReader.ReadToEnd();
				await _storeProductService.UploadPriceFeed(csvContent);
			}

			return Ok();
		}

		/// <summary>
		/// Get all storeProducts
		/// </summary>
		/// <returns>Returns all storeProducts</returns>
		[HttpPost("list")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult<PaginationResponse<StoreProductModel>>> List(GetStoreProductList request)
		{
			return Ok(await _storeProductService.List(request));
		}

		/// <summary>
		/// Get StoreProduct by Id
		/// </summary>
		/// <param name="id">StoreProduct Id</param>
		/// <returns>Returns StoreProduct detail by id</returns>
		[HttpGet("{id:int}")]
		public async Task<ActionResult<StoreProductModel>> Get(int id)
		{
			return Ok(await _storeProductService.GetById(id));
		}

		/// <summary>
		/// Update existing storeProduct
		/// </summary>
		/// <param name="request">storeProduct info</param>
		/// <returns></returns>
		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult> Update(UpdateStoreProduct request)
		{
			return Ok(await _storeProductService.Update(request));
		}
	}
}