using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;


namespace User_and_Task_API.Models
{
    public class User
    {
        [SwaggerSchema("Primary key for the User")]
        public int ID { get; set; }

        [SwaggerSchema("Unique username of the user")]
        public String UserName { get; set; }

         [SwaggerSchema("User's email address")]
        public String Email { get; set; }

        [SwaggerSchema("Password (should be hashed in real apps)")]
        public String Password { get; set; }

    }
}