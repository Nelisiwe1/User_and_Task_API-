using Microsoft.EntityFrameworkCore;
using User_and_Task_API.Models;

namespace User_and_Task_API.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
