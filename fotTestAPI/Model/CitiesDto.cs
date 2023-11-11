namespace fotTestAPI.Model
{
    public class CitiesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int NumberPointsOfInterests
        {
            get { return pointsOfInterests.Count; } // Return the private field
        }

        public ICollection<PointOfInterestDto> pointsOfInterests { get; set; } = new List<PointOfInterestDto>();

       
    }
}
 