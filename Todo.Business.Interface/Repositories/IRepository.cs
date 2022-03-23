using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Common.Interface;

namespace Todo.Business.Interface
{
	public interface IRepository<TEntity>
		where TEntity: class, IIdentifiableEntity, new()
	{
		public Task<TEntity> AddAsync(TEntity entity);
		public Task AddRange(IEnumerable<TEntity> entities);
		public void Remove(TEntity entity);
		public void Remove(long id);
		public TEntity Update(TEntity entity);
		public TEntity Find(long id);
		public IEnumerable<TEntity> GetAllEntities();
	}
}
