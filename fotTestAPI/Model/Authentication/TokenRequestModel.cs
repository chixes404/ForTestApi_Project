using System.ComponentModel.DataAnnotations;

namespace fotTestAPI.Model.Authentication
{
	public class TokenRequestModel
	{

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

	}
}
