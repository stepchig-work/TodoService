﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace Todo.DataAccess
{
	public static class AppContextService
	{
		public static IServiceCollection AddAppContext(this IServiceCollection services, string connections)
		{
			services.AddDbContext<TodoContext>(options =>
				options.UseSqlServer(connections));
			return services;
		}
	}
}
