using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using StoreManagement.Common.Helper;
using StoreManagement.Data.Context;
using StoreManagement.Data.Entities;
using StoreManagement.Data.Enums;
using Role = StoreManagement.Data.Enums.Role;

namespace StoreManagement.Data.Seed
{
	public static class DataSeeder
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
			var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

			//don't call migrate in test environment
			if (env.IsEnvironment("Test"))
			{
				dbContext.Database.EnsureDeleted();
				dbContext.Database.EnsureCreated();
			}
			else
			{
				dbContext.Database.Migrate();
			}


			#region Country

			if (!dbContext.Countries.Any())
			{
				dbContext.Countries.Add(new Country()
				{
					Name = "United States",
					IsoCode = "USA",
					DisplayOrder = 1
				});
				dbContext.Countries.Add(new Country()
				{
					Name = "India",
					IsoCode = "IND",
					DisplayOrder = 2
				});
				dbContext.SaveChanges();
			}

			#endregion

			#region State

			if (!dbContext.StateProvinces.Any())
			{
				dbContext.StateProvinces.Add(new StateProvince()
				{
					CountryId = 1,
					Name = "Dallas",
					Abbreviation = "DAL",
					DisplayOrder = 1
				});
				dbContext.StateProvinces.Add(new StateProvince()
				{
					CountryId = 2,
					Name = "Gujarat",
					Abbreviation = "GUJ",
					DisplayOrder = 2
				});
				dbContext.StateProvinces.Add(new StateProvince()
				{
					CountryId = 2,
					Name = "Maharashtra",
					Abbreviation = "MAH",
					DisplayOrder = 3
				});
				dbContext.SaveChanges();
			}

			#endregion

			#region City

			if (!dbContext.Cities.Any())
			{
				dbContext.Cities.Add(new City()
				{
					Name = "Irving, TX",
					StateProvinceId = 1,
					Abbreviation = "",
					DisplayOrder = 1
				});

				dbContext.Cities.Add(new City()
				{
					Name = "Dallas, TX",
					StateProvinceId = 1,
					Abbreviation = "",
					DisplayOrder = 2
				});

				dbContext.Cities.Add(new City()
				{
					Name = "Vadodara",
					StateProvinceId = 3,
					Abbreviation = "",
					DisplayOrder = 3
				});

				dbContext.Cities.Add(new City()
				{
					Name = "Ahmedabad",
					StateProvinceId = 3,
					Abbreviation = "",
					DisplayOrder = 4
				});

				dbContext.SaveChanges();
			}

			#endregion


			#region Store

			if (!dbContext.Stores.Any())
			{
				dbContext.Stores.Add(new Store()
				{
					Name = "Store 1",
					CountryId = 1,
					StateProvinceId = 1,
					CityId = 1
				});

				dbContext.Stores.Add(new Store()
				{
					Name = "Store 2",
					CountryId = 1,
					StateProvinceId = 2,
					CityId = 2
				});

				dbContext.Stores.Add(new Store()
				{
					Name = "Store 3",
					CountryId = 2,
					StateProvinceId = 3,
					CityId = 3
				});

				dbContext.Stores.Add(new Store()
				{
					Name = "Store 4",
					CountryId = 2,
					StateProvinceId = 3,
					CityId = 4
				});

				dbContext.SaveChanges();
			}

			#endregion

		}
	}
}