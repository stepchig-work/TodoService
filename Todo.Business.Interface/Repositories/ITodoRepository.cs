
using Todo.Common.Interface;
using Todo.Presentation.Entities;

namespace Todo.Business.Interface
{
	public interface ITodoRepository: IRepository<TodoItem>
	{
	}
}
