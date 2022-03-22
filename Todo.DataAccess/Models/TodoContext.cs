
using Microsoft.EntityFrameworkCore;
using Todo.Business.Entities.Models;

namespace Todo.DataAccess.Models
{
    public class TodoContext : DbContext
    {

        public TodoContext() { }

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }


        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    }
}