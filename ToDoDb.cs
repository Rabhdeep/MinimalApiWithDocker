using Microsoft.EntityFrameworkCore;

namespace TodoAPI
{
    public class ToDoDb:DbContext
    {
        public ToDoDb(DbContextOptions<ToDoDb> options):base(options)
        {
        }
        public DbSet<TodoItem> Todos { get; set; }
    }
}
