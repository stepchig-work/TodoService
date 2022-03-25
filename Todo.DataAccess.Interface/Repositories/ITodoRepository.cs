
using Todo.Business.Entities;
using Todo.Common.Interface;

namespace Todo.DataAccess.Interface
{
	public interface ITodoRepository: IRepository<TodoItem>	{ }
}
