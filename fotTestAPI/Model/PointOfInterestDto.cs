using System.Collections.Generic;

namespace fotTestAPI.Model
{
    public class PointOfInterestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } =string.Empty;
        public string? Description { get; set; }

        // You probably intended to create a property to access the list of points of interests.
        // However, it's better to use a field to store the list and provide a method to access it.
       

    }
}
