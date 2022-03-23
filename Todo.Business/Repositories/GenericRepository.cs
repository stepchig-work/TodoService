using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

using AutoMapper;
using Todo.Business.Interface;
using Todo.Common.Interface;


namespace Todo.Business
{
	public abstract class GenericRepository<TInnerRepository, TClientEntity, TBusinessEntity> : IRepository<TClientEntity>
		where TClientEntity : class, IIdentifiableEntity, new()
		where TBusinessEntity : class, IIdentifiableEntity, new()
		where TInnerRepository : IRepository<TBusinessEntity>
	{
		protected IMapper mapper;
		protected TInnerRepository innerRepository;
		public GenericRepository(IMapper mapper, TInnerRepository innerRepository)
		{
			Contract.Requires(mapper != null);
			Contract.Requires(innerRepository != null);

			this.mapper = mapper;
			this.innerRepository = innerRepository;
		}
		public async Task<TClientEntity> AddAsync(TClientEntity entity)
		{
			var addedEntity = mapper.Map<TBusinessEntity>(entity);
			var resultEntity = await innerRepository.AddAsync(addedEntity);
			return mapper.Map<TClientEntity>(resultEntity);
		}

		public async Task AddRangeAsync(IEnumerable<TClientEntity> entities)
		{
			var addedEntities = new List<TBusinessEntity>();
			foreach (var entity in entities)
			{
				addedEntities.Add(mapper.Map<TBusinessEntity>(entity));
			}
			await innerRepository.AddRangeAsync(addedEntities);
		}

		public async Task<TClientEntity> FindAsync(long id)
		{
			var result = await innerRepository.FindAsync(id);
			return mapper.Map<TClientEntity>(result);
		}

		public async Task<IEnumerable<TClientEntity>> GetAllEntitiesAsync()
		{
			var allEntities = await innerRepository.GetAllEntitiesAsync();
			var result = new List<TClientEntity>();
			foreach (var entity in allEntities)
			{
				result.Add(mapper.Map<TClientEntity>(entity));
			}
			return result;
		}

		public async Task RemoveAsync(TClientEntity entity)
		{
			var removeEntity = mapper.Map<TBusinessEntity>(entity);
			await innerRepository.RemoveAsync(removeEntity);
		}

		public async Task RemoveAsync(long id)
		{
			await innerRepository.RemoveAsync(id);
		}

		public async Task<TClientEntity> UpdateAsync(TClientEntity entity)
		{
			var updateEntity = mapper.Map<TBusinessEntity>(entity);
			var resultEntity = await innerRepository.UpdateAsync(updateEntity);
			return mapper.Map<TClientEntity>(resultEntity);
		}
	}
}
