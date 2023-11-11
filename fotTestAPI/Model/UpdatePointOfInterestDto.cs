using System.ComponentModel.DataAnnotations;

namespace fotTestAPI.Model
{
	public class UpdatePointOfInterestDto
	{


		[Required(ErrorMessage = "enter your name please")]
		[MaxLength(100)]
		public string Name { get; set; } = string.Empty;
		[Required]
		[MaxLength(200)]
		public string? Description { get; set; }


	}
}
