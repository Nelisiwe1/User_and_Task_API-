using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations

namespace User_and_Task_API.Models
{
    public class UserTask
    {
        [SwaggerSchema("Primary key for the Task")]
        public int Id { get; set; }
        
        [SwaggerSchema("Title of the task")]
        public string Title { get; set; }

        
        [SwaggerSchema("Optional description of the task")]
        public string? Description { get; set; }

         [SwaggerSchema("ID of the assigned User")]
         public int Assignee { get; set; }

         [SwaggerSchema("ID of the assigned User")]
        public DateTime DueDate { get; set; }
        
    }
}
