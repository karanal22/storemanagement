using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StoreManagement.Data.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace StoreManagement.Data.Context
{
	public class ApplicationDbContext : DbContext
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
			: base(options)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			int stringMaxLength = 256;
			// User IdentityRole and IdentityUser in case you haven't extended those classes


			// We are using int here because of the change on the PK
			//builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(stringMaxLength));
			//builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.ProviderKey).HasMaxLength(stringMaxLength));

			//// We are using int here because of the change on the PK
			//builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(stringMaxLength));
			//builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.Name).HasMaxLength(stringMaxLength));

			foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}

			//builder
			//	.Entity<AccessMatrix>()
			//	.Property(e => e.Key)
			//	.HasConversion<string>();
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		{
			var entries = ChangeTracker
				.Entries()
				.Where(e => (e.Entity is BaseIdLogEntity || e.Entity is BaseLogEntity)
							&& (e.State == EntityState.Added || e.State == EntityState.Modified)).ToList();

			var httpContext = _httpContextAccessor.HttpContext;

			foreach (var entityEntry in entries)
			{
				entityEntry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;

				if (entityEntry.State == EntityState.Added)
				{
					entityEntry.Property("CreatedAt").CurrentValue = DateTime.Now;
				}
			}

			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		public DbSet<Country> Countries { get; set; }
		public DbSet<StateProvince> StateProvinces { get; set; }
		public DbSet<City> Cities { get; set; }

		public DbSet<Store> Stores { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<StoreProduct> StoreProducts { get; set; }
	}
}
