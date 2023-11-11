using System.ComponentModel.DataAnnotations;

namespace fotTestAPI.Model
{
	public class CreatePOintOfInterestDto
	{


		[Required(ErrorMessage="enter your name please")]
		[MaxLength(100)]
		public string Name { get; set; } = string.Empty;
		[Required]
		[MaxLength(200)]
		public string? Description { get; set; }


	}
}
