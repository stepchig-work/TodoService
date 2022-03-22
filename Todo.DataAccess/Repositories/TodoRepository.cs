

using AutoMapper;
using System.Linq;
using Todo.Business.Entities.Models;
using Todo.DataAccess.Interface.Repositories;
using Todo.DataAccess.Models;

namespace Todo.DataAccess.Repositories
{
	public class TodoRepository : BaseRepository<TodoContext, TodoItem>, ITodoRepository
	{
		public TodoRepository(IMapper mapper): base(mapper) { }
		protected override TodoItem FindById(long id, TodoContext dbContext) => dbContext.TodoItems.FirstOrDefault(todo => todo.Id == id);
		
	}
}
