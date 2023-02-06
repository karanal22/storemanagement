using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Dynamic;
using System.Threading;

namespace StoreManagement.Data.Context
{
	public readonly struct DapperEFCoreCommand : IDisposable
	{
		private readonly ILogger<DapperEFCoreCommand> _logger;
		private readonly IHttpContextAccessor _httpContextAccessor;


		public DapperEFCoreCommand(
			DbContext context,
			string text,
			object parameters,
			int? timeout,
			CommandType? type,
			CancellationToken ct
		)
		{
			SqlMapper.AddTypeMap(typeof(DateTime), System.Data.DbType.DateTime2);
			_logger = context.GetService<ILogger<DapperEFCoreCommand>>();
			_httpContextAccessor = context.GetService<IHttpContextAccessor>();

			var transaction = context.Database.CurrentTransaction?.GetDbTransaction();
			var commandType = type ?? CommandType.Text;
			var commandTimeout = timeout ?? context.Database.GetCommandTimeout() ?? 30;
			//dynamic parameters1 = new ExpandoObject();
			//parameters1 = parameters;
			//if (typeof(object).IsAssignableFrom(typeof(Entities.BaseIdLogEntity)))
			//{
			//	if (parameters != null)
			//	{
			//		parameters1.CreatedAt = DateTime.Now;
			//		//parameters1.UpdatedAt = DateTime.Now;
			//		var httpContext = _httpContextAccessor.HttpContext;
			//		if (httpContext != null)
			//		{
			//			var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("userId")?.Value;
			//			if (int.TryParse(userIdClaim, out int authenticatedUserId))
			//			{
			//				//parameters1.UpdatedBy = authenticatedUserId;
			//				parameters1.CreatedBy = authenticatedUserId;
			//			}
			//		}
			//	}
			//}

			Definition = new CommandDefinition(
				text,
				parameters,
				transaction,
				commandTimeout,
				commandType,
				cancellationToken: ct
			);


			if (_logger.IsEnabled(LogLevel.Debug))
			{
				_logger.LogDebug(
					@"Executing DbCommand [CommandType='{commandType}', CommandTimeout='{commandTimeout}']
					{commandText}", Definition.CommandType, Definition.CommandTimeout, Definition.CommandText);
			}
		}

		public CommandDefinition Definition { get; }

		public void Dispose()
		{
			if (_logger.IsEnabled(LogLevel.Information))
			{
				_logger.LogInformation(
					@"Executed DbCommand [CommandType='{commandType}', CommandTimeout='{commandTimeout}']
					{commandText}", Definition.CommandType, Definition.CommandTimeout, Definition.CommandText);
			}
		}
	}
}