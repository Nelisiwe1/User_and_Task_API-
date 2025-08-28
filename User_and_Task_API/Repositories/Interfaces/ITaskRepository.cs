using User_and_Task_API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace User_and_Task_API.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<UserTask>> GetAllAsync();
        Task<UserTask?> GetByIdAsync(int id);
        Task<UserTask> AddAsync(UserTask task);
        Task<UserTask> UpdateAsync(UserTask task);
        Task DeleteAsync(int id);

        Task<List<UserTask>> GetExpiredTasksAsync();
        Task<List<UserTask>> GetActiveTasksAsync();
        Task<List<UserTask>> GetTasksByDateAsync(DateTime date);
        Task<List<UserTask>> GetTasksByAssigneeAsync(int userId);
    }
}
