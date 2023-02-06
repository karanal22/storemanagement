using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace StoreManagement.Api.Controllers
{
	[ApiController]
	public class BaseController : ControllerBase
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public BaseController(IServiceProvider serviceProvider)
		{
			_httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
		}
	}
}