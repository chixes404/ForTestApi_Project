using System.ComponentModel.DataAnnotations;

namespace fotTestAPI.Model.Authentication
{
    public class RegisterModel
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(128)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }
    }
}
