using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Todo.Common.Interface;

namespace Todo.DataAccess
{
	public abstract class BaseRepository<TDbContext, TEntity> : IRepository<TEntity>
		where TEntity: class, IIdentifiableEntity, new()
		where TDbContext: DbContext, new()
		
	{
		protected readonly IMapper mapper;

		protected BaseRepository(IMapper mapper)
		{
			Contract.Requires(mapper != null);

			this.mapper = mapper;
		}
		protected abstract Task<TEntity> FindByIdAsync(long id, TDbContext dbContext);

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			ValidateEntity(entity);

			using (var dbContext = new TDbContext()){
				var added = await dbContext.Set<TEntity>().AddAsync(entity);
				dbContext.SaveChanges();

				return added.Entity;
			}
		}


		public void RemoveAsync(TEntity entity)
		{
			RemoveAsync(entity.Id);
		}

		public void RemoveAsync(long id)
		{
			using (var dbContext = new TDbContext())
			{
				var entity = FindByIdAsync(id, dbContext);
				dbContext.Entry(entity).State = EntityState.Deleted;
				dbContext.SaveChanges();
			}
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			using (var dbContext = new TDbContext())
			{
				var existingEntity = await FindByIdAsync(entity.Id, dbContext);
				mapper.Map(entity, existingEntity);
				dbContext.Entry(existingEntity).State = EntityState.Modified;
				dbContext.SaveChanges();
				return existingEntity;
			}
		}

		public async Task AddRangeAsync(IEnumerable<TEntity> entities)
		{

			using (var dbContext = new TDbContext())
			{
				await dbContext.Set<TEntity>().AddRangeAsync(entities);
				dbContext.SaveChanges();
			}
		}

		public async Task<TEntity> FindAsync(long id)
		{
			using (var dbContext = new TDbContext())
			{
				return await FindByIdAsync(id, dbContext);
			}
		}

		public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync()
		{
			using (var dbContext = new TDbContext())
			{
				return await dbContext.Set<TEntity>().ToListAsync();
			}
		}

		protected void ValidateEntity(TEntity entity)
		{
			if (entity.Id != 0)
			{
				throw new ValidationException("New entity Id cannot have a prederemined Id");
			}
		}		
	}
}

