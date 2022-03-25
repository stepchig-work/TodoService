using AutoMapper;
using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Todo.Common.Interface;

namespace Todo.DataAccess
{
	public abstract class BaseRepository<TDbContext, TEntity> : IRepository<TEntity>
		where TEntity : class, IIdentifiableEntity, new()
		where TDbContext : DbContext
	{
		protected readonly IMapper mapper;
		private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		protected readonly TDbContext dbContext;
		private bool disposed;

		protected BaseRepository(IMapper mapper, TDbContext dbContext)
		{
			Contract.Requires(mapper != null);
			Contract.Requires(dbContext != null);

			this.mapper = mapper;
			this.dbContext = dbContext;
		}
		protected abstract Task<TEntity> FindByIdAsync(long id, TDbContext dbContext);

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			ValidateEntity(entity);

			var added = await dbContext.Set<TEntity>().AddAsync(entity);
			dbContext.SaveChanges();

			return added.Entity;
		}


		public async Task RemoveAsync(TEntity entity)
		{
			await RemoveAsync(entity.Id);
		}

		public async Task RemoveAsync(long id)
		{
			var entity = await FindByIdAsync(id, dbContext);
			dbContext.Entry(entity).State = EntityState.Deleted;
			dbContext.SaveChanges();
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			var existingEntity = await FindByIdAsync(entity.Id, dbContext);
			mapper.Map(entity, existingEntity);
			dbContext.Entry(existingEntity).State = EntityState.Modified;
			dbContext.SaveChanges();
			return existingEntity;

		}

		public async Task AddRangeAsync(IEnumerable<TEntity> entities)
		{
			await dbContext.Set<TEntity>().AddRangeAsync(entities);
			dbContext.SaveChanges();
		}


		public async Task<TEntity> FindAsync(long id)
		{
			return await FindByIdAsync(id, dbContext);

		}

		public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync()
		{
			return await dbContext.Set<TEntity>().ToListAsync();

		}

		protected void ValidateEntity(TEntity entity)
		{
			if (entity.Id != 0)
			{
				log.Error("New entity Id cannot have a prederemined Id");
				throw new ValidationException("New entity Id cannot have a prederemined Id");
			}
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					dbContext.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}

