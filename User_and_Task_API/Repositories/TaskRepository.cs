using Microsoft.EntityFrameworkCore;
using User_and_Task_API.Data;
using User_and_Task_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<UserTask>> GetAllAsync() =>
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

        public async Task<List<UserTask>> GetExpiredTasksAsync() =>
            await _context.Tasks
                .Where(t => t.DueDate < DateTime.UtcNow)
                .ToListAsync();

        public async Task<List<UserTask>> GetActiveTasksAsync() =>
            await _context.Tasks
                .Where(t => t.DueDate >= DateTime.UtcNow)
                .ToListAsync();

        public async Task<List<UserTask>> GetTasksByDateAsync(DateTime date) =>
            await _context.Tasks
                .Where(t => t.DueDate.Date == date.Date)
                .ToListAsync();

        public async Task<List<UserTask>> GetTasksByAssigneeAsync(int userId) =>
            await _context.Tasks
                .Where(t => t.Assignee == userId)
                .ToListAsync();
    }
}
