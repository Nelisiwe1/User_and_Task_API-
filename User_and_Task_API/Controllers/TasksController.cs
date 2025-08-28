using Microsoft.AspNetCore.Mvc;
using User_and_Task_API.Models;
using User_and_Task_API.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace User_and_Task_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repo;

        public TasksController(ITaskRepository repo)
        {
            _repo = repo;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _repo.GetAllAsync();
            return Ok(tasks);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _repo.GetByIdAsync(id);
            if (task == null) return NotFound("Task not found");
            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask(UserTask task)
        {
            if (task == null || string.IsNullOrEmpty(task.Title))
                return BadRequest("Task and title are required");

            var createdTask = await _repo.AddAsync(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UserTask updatedTask)
        {
            var task = await _repo.GetByIdAsync(id);
            if (task == null) return NotFound("Task not found");

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Assignee = updatedTask.Assignee;
            task.DueDate = updatedTask.DueDate;

            var result = await _repo.UpdateAsync(task);
            return Ok(result);
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _repo.GetByIdAsync(id);
            if (task == null) return NotFound("Task not found");

            await _repo.DeleteAsync(id);
            return Ok("Task deleted");
        }

        // GET: api/tasks/expired
        [HttpGet("expired")]
        public async Task<IActionResult> GetExpiredTasks()
        {
            var tasks = await _repo.GetExpiredTasksAsync();
            if (tasks.Count == 0) return NotFound("No expired tasks found");
            return Ok(tasks);
        }

        // GET: api/tasks/active
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveTasks()
        {
            var tasks = await _repo.GetActiveTasksAsync();
            if (tasks.Count == 0) return NotFound("No active tasks found");
            return Ok(tasks);
        }

        // GET: api/tasks/by-date/{date}
        [HttpGet("by-date/{date}")]
        public async Task<IActionResult> GetTasksByDate(DateTime date)
        {
            var tasks = await _repo.GetTasksByDateAsync(date);
            if (tasks.Count == 0) return NotFound($"No tasks found for {date:yyyy-MM-dd}");
            return Ok(tasks);
        }

        // GET: api/tasks/by-assignee/{userId}
        [HttpGet("by-assignee/{userId}")]
        public async Task<IActionResult> GetTasksByAssignee(int userId)
        {
            var tasks = await _repo.GetTasksByAssigneeAsync(userId);
            if (tasks.Count == 0) return NotFound($"No tasks found for user {userId}");
            return Ok(tasks);
        }
    }
}
