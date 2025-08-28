using Microsoft.EntityFrameworkCore;
using User_and_Task_API.Data;
using User_and_Task_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using User_and_Task_API.Repositories.Interfaces; 



namespace User_and_Task_API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApiContext _context;

        public TaskRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserTask>> GetAllAsync() =>
            await _context.Tasks.ToListAsync();

        public async Task<UserTask?> GetByIdAsync(int id) =>
            await _context.Tasks.FindAsync(id);

        public async Task<UserTask> AddAsync(UserTask task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<UserTask> UpdateAsync(UserTask task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
        
        // ---------------- Filtering ----------------

        public async Task<IEnumerable<UserTask>> GetExpiredTasksAsync()
        {
            return await _context.Tasks
                .Where(t => t.DueDate < DateTime.Now)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserTask>> GetActiveTasksAsync()
        {
            return await _context.Tasks
                .Where(t => t.DueDate >= DateTime.Now)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserTask>> GetTasksByDateAsync(DateTime date)
        {
            return await _context.Tasks
                .Where(t => t.DueDate.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserTask>> GetTasksByAssigneeAsync(int userId)
        {
            return await _context.Tasks
                .Where(t => t.Assignee == userId)
                .ToListAsync();
        }
    }
}
