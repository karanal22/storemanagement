using NetCoreForce.Models;
using StoreManagement.Services.Model.Request.StoreProdect;
using StoreManagement.Services.Model.Response.StoreProduct;
using System;
using Profile = AutoMapper.Profile;

namespace StoreManagement.Services.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{

			CreateMap<PriceFeedModel, Data.Entities.StoreProduct>();

			CreateMap<UpdateStoreProduct, Data.Entities.StoreProduct>();
			CreateMap<Data.Entities.StoreProduct, StoreProductModel>()
				.ForMember(res => res.StoreName, opt => opt.MapFrom(ent => ent.Store.Name))
				.ForMember(res => res.StoreCity, opt => opt.MapFrom(ent => ent.Store.City.Name))
				.ForMember(res => res.ProductName, opt => opt.MapFrom(ent => ent.Product.Name));

		}
	}
}
