using System.ComponentModel.DataAnnotations;

namespace fotTestAPI.Model.Authentication
{
    public class AddRoleModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }


    }
}
