using AutoMapper;

using BusinessTodoItem = Todo.Business.Entities.TodoItem;

using PresentationTodoItem = Todo.Presentation.Entities.TodoItem;

namespace Todo.Common
{
	public class MappingProfile: Profile
	{
		public MappingProfile()
		{
				

			CreateMap<PresentationTodoItem, BusinessTodoItem>()
				.MaxDepth(1); ;
			CreateMap<BusinessTodoItem, PresentationTodoItem>()
				.MaxDepth(1);
			CreateMap<PresentationTodoItem, BusinessTodoItem>()
				.MaxDepth(1); ;
		}

	}
}
