using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fotTestAPI.Entites
{
	public class City
	{
		
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; } = string.Empty;
		[MaxLength(200)]
		public string? Description { get; set; }
		public int NumberPointsOfInterests
		{
			get { return pointsOfInterests.Count; } // Return the private field
		}
public ICollection<PointOfInterests> pointsOfInterests { get; set; } = new List<PointOfInterests>();







	}
}
