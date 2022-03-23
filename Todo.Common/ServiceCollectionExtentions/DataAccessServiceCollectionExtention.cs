
using Microsoft.Extensions.DependencyInjection;
using Todo.DataAccess;
using Todo.DataAccess.Interface;

namespace Todo.Common.ServiceCollectionExtentions
{
	public static class DataAccessServiceCollectionExtention
	{
		public static void ConfigureDataAccessProject(this IServiceCollection services)
		{
			ConfigureRepositories(services);
		}

		private static void ConfigureRepositories(IServiceCollection services)
		{
			services.AddTransient<ITodoRepository, TodoRepository>();
		}
	}
}
