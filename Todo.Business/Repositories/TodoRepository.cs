using Todo.Business.Interface;
using AutoMapper;

using BusinessTodoItem = Todo.Business.Entities.TodoItem;
using PresentationTodoItem = Todo.Presentation.Entities.TodoItem;

using DataRepository = Todo.DataAccess.Interface.ITodoRepository;

namespace Todo.Business
{
	public class TodoRepository : GenericRepository<DataRepository, PresentationTodoItem, BusinessTodoItem>,
		ITodoRepository
	{
		public TodoRepository(IMapper mapper, DataRepository repository) : base(mapper, repository) { }
	}
}
