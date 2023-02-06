using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using StoreManagement.Api.Infrastructure.Dependencies;
using StoreManagement.Common.ExceptionHandler;
using StoreManagement.Data.Configurations;
using StoreManagement.Data.Context;
using StoreManagement.Services.Mapper;

namespace StoreManagement.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Configuration = configuration;
			HostingEnvironment = environment;
		}

		public IConfiguration Configuration { get; }
		public IWebHostEnvironment HostingEnvironment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public virtual void ConfigureServices(IServiceCollection services)
		{
			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);


			#region CORS

			services.AddCors(options =>
			{
				options.AddPolicy(name: "CorsPolicy",
					builder =>
					{
						builder.AllowAnyOrigin();
						builder.AllowAnyHeader();
						builder.AllowAnyMethod();
					});
			});

			#endregion

			services.AddControllers();

			services.AddMvc().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.IgnoreNullValues = true;
			});

			services.AddSpaStaticFiles(options =>
			{
				options.RootPath = "wwwroot";
			});

			#region Dbcontext


			services.AddDbContext<ApplicationDbContext>(options => options.UseLazyLoadingProxies()
				   .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			#endregion

			#region Swagger

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "StoreManagement API", Version = "v1" });
				//use fully qualified object names
				c.CustomSchemaIds(x => x.FullName);
				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);

				var securitySchema = new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = HttpRequestHeader.Authorization.ToString(),
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer",
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				};
				c.AddSecurityDefinition("Bearer", securitySchema);

				var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
				c.AddSecurityRequirement(securityRequirement);
			});

			#endregion

			var appSettings = appSettingsSection.Get<AppSettings>();


			#region Dependency Registration

			services.RegisterDependency();

			#endregion

			#region Auto mapper

			services.InitializeAutoMapper();

			#endregion

			#region Disbaled automatic modelstate validation

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory =
					actionContext => throw new AppException(actionContext.ModelState);
			});

			#endregion
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});

			loggerFactory.AddLog4Net();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.ConfigureExceptionHandler(loggerFactory);

			app.UseSpaStaticFiles();
			#region Static files

			app.UseStaticFiles(); // For the wwwroot folder

			#endregion

			app.UseRouting();

			app.UseCors("CorsPolicy");

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "Default",
					pattern: "/"//,
								//defaults: new { controller = "Home", action = "Index", }
					);

				endpoints.MapControllers();
			});

			app.UseSwagger();

			#region Culture info

			var supportedCultures = new[] { new CultureInfo("en-IN") };
			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("en-IN"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures
			});

			#endregion

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreManagement API");
			});

			//// This sequence is important this will always redirect to angular application
			//// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.spaapplicationbuilderextensions.usespa?view=aspnetcore-3.0
			//// https://stackoverflow.com/questions/62053674/how-to-host-an-angular-app-inside-net-core-3-1-webapi
			//app.UseSpa(spa =>
			//{
			//	// To learn more about options for serving an Angular SPA from ASP.NET Core,
			//	// see https://go.microsoft.com/fwlink/?linkid=864501
			//});
		}
	}
}
