using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StoreManagement.Data.Repository;
using StoreManagement.Services.Service.StoreProduct;

namespace StoreManagement.Api.Infrastructure.Dependencies
{
	public static class DependencyRegistrar
	{
		public static void RegisterDependency(this IServiceCollection services)
		{
			//Repositories
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<IStoreProductService, StoreProductService>();

		}
	}
}