using User_and_Task_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace User_and_Task_API.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<UserTask>> GetAllAsync();
        Task<UserTask?> GetByIdAsync(int id);
        Task<UserTask> AddAsync(UserTask task);
        Task<UserTask> UpdateAsync(UserTask task);
        Task DeleteAsync(int id);

        Task<IEnumerable<UserTask>> GetExpiredTasksAsync();
    Task<IEnumerable<UserTask>> GetActiveTasksAsync();
    Task<IEnumerable<UserTask>> GetTasksByDateAsync(DateTime date);
    Task<IEnumerable<UserTask>> GetTasksByAssigneeAsync(int userId);
    }
}
