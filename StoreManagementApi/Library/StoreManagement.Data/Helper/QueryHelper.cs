using Castle.Core.Internal;
using StoreManagement.Common.Model.Request;
using StoreManagement.Common.Model.Response;
using StoreManagement.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StoreManagement.Data.Helper
{
	public static class QueryHelper
	{
		public static PaginationResponse<R> GetPagination<T, R>(this IRepository<T> repository,
			Func<T, R> selector,
			List<Expression<Func<T, bool>>> filterList,
			PaginationRequest paginationRequest)
		{
			var entities = repository.GetCollection(filterList, paginationRequest).ToList()
				.Select(selector).ToList();
			var count = repository.Count(filterList);
			return new PaginationResponse<R>(entities, count, paginationRequest.PageNumber, paginationRequest.PageSize);
		}

		public static PaginationResponse<R> GetPaginationAsNoTracking<T, R>(this IRepository<T> repository,
			Func<T, R> selector,
			List<Expression<Func<T, bool>>> filterList,
			PaginationRequest paginationRequest)
		{
			var entities = repository.GetCollectionAsNoTracking(filterList, paginationRequest).ToList()
				.Select(selector).ToList();
			var count = repository.Count(filterList);
			return new PaginationResponse<R>(entities, count, paginationRequest.PageNumber, paginationRequest.PageSize);
		}

		public static IQueryable<T> CustomOrderBy<T>(this IQueryable<T> source, List<Dictionary<string, string>> sortBy)
		{
			if (sortBy.IsNullOrEmpty())
			{
				return source;
			}

			var expression = source.Expression;

			int count = 0;
			foreach (var item in sortBy)
			{
				var parameter = Expression.Parameter(typeof(T), "x");
				var sort = item.First();
				var selector = CreateNestedMemberExpression(parameter, sort.Key);
				var method = string.Equals(sort.Value, "desc", StringComparison.OrdinalIgnoreCase)
					? (count == 0 ? "OrderByDescending" : "ThenByDescending")
					: (count == 0 ? "OrderBy" : "ThenBy");
				expression = Expression.Call(typeof(Queryable), method,
					new Type[] { source.ElementType, selector.Type },
					expression, Expression.Quote(Expression.Lambda(selector, parameter)));
				count++;
			}

			return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
		}

		static MemberExpression CreateNestedMemberExpression(ParameterExpression parameter, string propertyName)
		{
			var propertyPath = propertyName.Split('.');

			MemberExpression memberExpression = Expression.Property(parameter, propertyPath[0]);

			foreach (var member in propertyPath.Skip(1))
			{
				memberExpression = Expression.Property(memberExpression, member);
			}

			return memberExpression;
		}
	}
}