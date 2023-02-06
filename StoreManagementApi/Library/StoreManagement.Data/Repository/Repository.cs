using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Common.Model.Request;
using StoreManagement.Data.Context;
using StoreManagement.Data.Helper;

namespace StoreManagement.Data.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _dbContext;
		private DbSet<T> _entities;

		public Repository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<IList<T>> GetAllAsync()
		{
			var query = Table;
			return await query.ToListAsync();
		}

		public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
		{
			var query = Table;
			var data = await query.Where(predicate).ToListAsync();
			return data;
		}

		public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
		{
			var query = Table;
			var data = await query.FirstOrDefaultAsync(predicate);
			return data;
		}

		public async Task<T> InsertAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			try
			{
				_dbContext.Set<T>().Remove(entity);
				await _dbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				// concurrency exception is ignored here
			}
		}

		public async Task UpdateAsync(T entity)
		{
			try
			{
				_dbContext.Entry(entity).State = EntityState.Modified;
				await _dbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				// concurrency exception is ignored here
			}
		}
		public async Task<T> UpdateAsync2(T entity)
		{
			try
			{
				_dbContext.Entry(entity).State = EntityState.Modified;
				await _dbContext.SaveChangesAsync();
				return entity;
			}
			catch (DbUpdateConcurrencyException)
			{
				// concurrency exception is ignored here
			}
			return null;
		}
		public async Task DeleteRangeAsync(List<T> entityList)
		{
			try
			{
				_dbContext.Set<T>().RemoveRange(entityList);
				await _dbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				// concurrency exception is ignored here
			}
		}

		public async Task AddRangeAsync(List<T> entityList)
		{
			try
			{
				await _dbContext.Set<T>().AddRangeAsync(entityList);
				await _dbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				// concurrency exception is ignored here
			}
		}

		public async Task UpdateRangeAsync(List<T> entityList)
		{
			try
			{
				_dbContext.Set<T>().UpdateRange(entityList);
				await _dbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				// concurrency exception is ignored here
			}
		}

		public IEnumerable<T> GetCollection(List<Expression<Func<T, bool>>> filterList,
			PaginationRequest paginationRequest)
		{
			var query = Table;
			filterList.ForEach(filter => { query = query.Where(filter); });
			query = query.CustomOrderBy(paginationRequest.SortBy);
			return query.Skip(paginationRequest.GetSkip()).Take(paginationRequest.GetTake()).ToList();
		}

		public IEnumerable<T> GetCollectionAsNoTracking(List<Expression<Func<T, bool>>> filterList,
		   PaginationRequest paginationRequest)
		{
			var query = TableNoTracking;
			filterList.ForEach(filter => { query = query.Where(filter); });
			query = query.CustomOrderBy(paginationRequest.SortBy);
			return query.Skip(paginationRequest.GetSkip()).Take(paginationRequest.GetTake()).ToList();
		}

		public int Count(List<Expression<Func<T, bool>>> filterList)
		{
			var query = Table;
			filterList.ForEach(filter => { query = query.Where(filter); });
			return query.Count();
		}

		/// <summary>
		/// Gets an entity set
		/// </summary>
		protected virtual DbSet<T> Entities
		{
			get
			{
				if (_entities == null)
					_entities = _dbContext.Set<T>();

				return _entities;
			}
		}

		/// <summary>
		/// Gets a table
		/// </summary>
		public virtual IQueryable<T> Table => Entities;

		/// <summary>
		/// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
		/// </summary>
		public virtual IQueryable<T> TableNoTracking => Entities.AsNoTracking();
	}
}