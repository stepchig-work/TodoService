using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Todo.Common;
using Todo.DataAccess.Interface.Repositories;

namespace Todo.DataAccess.Repositories
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
		protected abstract TEntity FindById(long id, TDbContext dbContext);

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			ValidateEntity(entity);

			using (var dbContext = new TDbContext()){
				var added = await dbContext.Set<TEntity>().AddAsync(entity);
				dbContext.SaveChanges();

				return added.Entity;
			}
		}


		public void Remove(TEntity entity)
		{
			Remove(entity.Id);
		}

		public void Remove(long id)
		{
			using (var dbContext = new TDbContext())
			{
				var entity = FindById(id, dbContext);
				dbContext.Entry(entity).State = EntityState.Deleted;
				dbContext.SaveChanges();
			}
		}

		public TEntity Update(TEntity entity)
		{
			using (var dbContext = new TDbContext())
			{
				var existingEntity = FindById(entity.Id, dbContext);
				mapper.Map(entity, existingEntity);
				dbContext.Entry(existingEntity).State = EntityState.Modified;
				dbContext.SaveChanges();
				return existingEntity;
			}
		}

		public async Task AddRange(IEnumerable<TEntity> entities)
		{

			using (var dbContext = new TDbContext())
			{
				await dbContext.Set<TEntity>().AddRangeAsync(entities);
				dbContext.SaveChanges();
			}
		}

		public TEntity Find(long id)
		{
			using (var dbContext = new TDbContext())
			{
				return FindById(id, dbContext);
			}
		}

		public IEnumerable<TEntity> GetAllEntities()
		{
			using (var dbContext = new TDbContext())
			{
				return dbContext.Set<TEntity>().ToList();
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

