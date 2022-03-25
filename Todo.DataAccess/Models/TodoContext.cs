
using Microsoft.EntityFrameworkCore;
using Todo.Business.Entities;

namespace Todo.DataAccess
{
	public class TodoContext : DbContext
	{
		public TodoContext(DbContextOptions<TodoContext> options)
			: base(options)
		{ }

		public DbSet<TodoItem> TodoItems { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder) { }
	}
}