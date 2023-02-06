using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace StoreManagement.Data.Context
{
	public static class DapperDbContextExtensions
	{
		public static async Task<IEnumerable<T>> QueryAsync<T>(
			this DbContext context,
			string text,
			object parameters = null,
			int? timeout = null,
			CommandType? type = null,
			CancellationToken ct = default
		)
		{
			using var command = new DapperEFCoreCommand(
				context,
				text,
				parameters,
				timeout,
				type,
				ct
			);

			var connection = context.Database.GetDbConnection();
			return await connection.QueryAsync<T>(command.Definition);
		}

		public static async Task<int> ExecuteAsync(
			this DbContext context,
			string text,
			object parameters = null,
			int? timeout = null,
			CommandType? type = null,
			CancellationToken ct = default
		)
		{
			using var command = new DapperEFCoreCommand(
				context,
				text,
				parameters,
				timeout,
				type,
				ct
			);

			var connection = context.Database.GetDbConnection();
			return await connection.ExecuteAsync(command.Definition);
		}


		public static async Task<IEnumerable<T>> QuerySPAsync<T>(
		this DbContext context,
		string text,
		object parameters = null,
		int? timeout = null,
		CancellationToken ct = default
	)
		{
			using var command = new DapperEFCoreCommand(
				context,
				text,
				parameters,
				timeout,
				CommandType.StoredProcedure,
				ct
			);

			var connection = context.Database.GetDbConnection();
			return await connection.QueryAsync<T>(command.Definition);
		}

		public static async Task<int> ExecuteSPAsync(
			this DbContext context,
			string text,
			object parameters = null,
			int? timeout = null,
			CancellationToken ct = default
		)
		{
			using var command = new DapperEFCoreCommand(
				context,
				text,
				parameters,
				timeout,
				CommandType.StoredProcedure,
				ct
			);

			var connection = context.Database.GetDbConnection();
			return await connection.ExecuteAsync(command.Definition);
		}


		public static async Task<T> QuerySPFirstOrDefaultAsync<T>(
		this DbContext context,
		string text,
		object parameters = null,
		int? timeout = null,
		CancellationToken ct = default
	)
		{
			using var command = new DapperEFCoreCommand(
				context,
				text,
				parameters,
				timeout,
				CommandType.StoredProcedure,
				ct
			);

			var connection = context.Database.GetDbConnection();
			return await connection.QueryFirstOrDefaultAsync<T>(command.Definition);
		}

	}
}