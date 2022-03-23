

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Todo.Business.Entities;
using Todo.DataAccess.Interface;

namespace Todo.DataAccess
{
	public class TodoRepository : BaseRepository<TodoContext, TodoItem>, ITodoRepository
	{
		public TodoRepository(IMapper mapper): base(mapper) { }
		protected override async Task<TodoItem> FindByIdAsync(long id, TodoContext dbContext) => 
			await dbContext.TodoItems.FirstOrDefaultAsync(todo => todo.Id == id);
		
	}	
}
