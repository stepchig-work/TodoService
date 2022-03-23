

using AutoMapper;
using System.Linq;
using Todo.Business.Entities;
using Todo.DataAccess.Interface;

namespace Todo.DataAccess
{
	public class TodoRepository : BaseRepository<TodoContext, TodoItem>, ITodoRepository
	{
		public TodoRepository(IMapper mapper): base(mapper) { }
		protected override TodoItem FindById(long id, TodoContext dbContext) => dbContext.TodoItems.FirstOrDefault(todo => todo.Id == id);
		
	}
}
