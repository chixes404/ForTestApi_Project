using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fotTestAPI.Entites
{
	public class PointOfInterests
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; } = string.Empty;
		[MaxLength(200)]
		public string? Description { get; set; }



		[Required]
		[ForeignKey("City")]
		public int CityId_v2 { get; set; }

		public City City { get; set; }

	}
}
