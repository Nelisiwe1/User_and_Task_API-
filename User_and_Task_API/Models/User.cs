using System.ComponentModel.DataAnnotations;

namespace User_and_Task_API.Models
{
    public class User
    {

        public int ID { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

    }
}