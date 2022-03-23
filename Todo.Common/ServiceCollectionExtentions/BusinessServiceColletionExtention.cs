using Microsoft.Extensions.DependencyInjection;
using Todo.Business;
using Todo.Business.Interface;

namespace Todo.Common.ServiceCollectionExtentions
{
	public static class BusinessServiceColletionExtention
	{
		public static void ConfigureBusinessProject(this IServiceCollection services)
		{
			ConfigureRepositories(services);
		}


		private static void ConfigureRepositories(IServiceCollection services)
		{
			services.AddScoped<ITodoRepository, TodoRepository>();
		}
	}
}
