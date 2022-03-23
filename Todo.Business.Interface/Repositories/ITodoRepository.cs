
using PresentationTodoItem = Todo.Presentation.Entities.TodoItem;
using BusinessTodoItem = Todo.Business.Entities.TodoItem;

namespace Todo.Business.Interface
{
	public interface ITodoRepository: IRepository<PresentationTodoItem>
	{
	}
}
