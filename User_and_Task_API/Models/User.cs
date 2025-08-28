using System.ComponentModel.DataAnnotations;

namespace User_and_Task_API.Models
{
    public class User
    {

        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}