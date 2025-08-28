using User_and_Task_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace User_and_Task_API.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<UserTask>> GetAllAsync();
        Task<UserTask> GetByIdAsync(int id);
        Task<UserTask> AddAsync(UserTask task);
        Task<UserTask> UpdateAsync(UserTask task);
        Task DeleteAsync(int id);
    }
}
