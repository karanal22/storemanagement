using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StoreManagement.Common.Model.Request;

namespace StoreManagement.Data.Repository
{
	public interface IRepository<T>
	{
		Task<T> GetByIdAsync(int id);

		Task<IList<T>> GetAllAsync();

		Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

		Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
		Task<T> InsertAsync(T entity);

		Task DeleteAsync(T entity);

		Task UpdateAsync(T entity);
		Task<T> UpdateAsync2(T entity);

		Task DeleteRangeAsync(List<T> entityList);

		Task AddRangeAsync(List<T> entityList);

		Task UpdateRangeAsync(List<T> entityList);

		IEnumerable<T> GetCollection(List<Expression<Func<T, bool>>> filterList, PaginationRequest paginationRequest);

		IEnumerable<T> GetCollectionAsNoTracking(List<Expression<Func<T, bool>>> filterList, PaginationRequest paginationRequest);

		int Count(List<Expression<Func<T, bool>>> filterList);

		#region properties

		/// <summary>
		/// Gets a table
		/// </summary>
		IQueryable<T> Table { get; }

		/// <summary>
		/// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
		/// </summary>
		IQueryable<T> TableNoTracking { get; }

		#endregion
	}
}
