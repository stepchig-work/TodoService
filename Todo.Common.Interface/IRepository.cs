using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Common.Interface
{
	public interface IRepository<TEntity>
		where TEntity : class, IIdentifiableEntity, new()
	{
		public Task<TEntity> AddAsync(TEntity entity);
		public Task AddRangeAsync(IEnumerable<TEntity> entities);
		public Task RemoveAsync(TEntity entity);
		public Task RemoveAsync(long id);
		public Task<TEntity> UpdateAsync(TEntity entity);
		public Task<TEntity> FindAsync(long id);
		public Task<IEnumerable<TEntity>> GetAllEntitiesAsync();
	}
}
