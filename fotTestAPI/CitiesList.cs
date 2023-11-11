using fotTestAPI.Model;

namespace fotTestAPI
{
    public class CitiesList
    {

        public List<CitiesDto> Cities { get; set; }

      //  public static CitiesList Current = new CitiesList();

        
        public CitiesList()
        {
            Cities = new List<CitiesDto>()
            {
                new CitiesDto() { Id = 1, Name = "cairo", Description = "bad city"
      
                , pointsOfInterests=new List<PointOfInterestDto>()
      {
            new PointOfInterestDto()
          {
              Id=1,
              Name="Cairo Tower",
              Description="fuck you man "
          },
             new PointOfInterestDto()
          {
              Id=2,
              Name="Pyramids",
              Description="fuck you man "
          },
                new PointOfInterestDto()
          {
              Id=3,
              Name="Luxor",
              Description="fuck you man "
          },
      }          
                
                
                }   ,
                new CitiesDto() { Id = 2, Name = "tanta", Description = "fuckin bad city"
                 , pointsOfInterests=new List<PointOfInterestDto>()
      {
          new PointOfInterestDto()
          {
              Id=2  ,
              Name="Elsayed ElBadway",
              Description="fuck you man "
          },
      }          },
                new CitiesDto() { Id = 3, Name = "alex", Description = "good city"
                 , pointsOfInterests=new List<PointOfInterestDto>()
      {
          new PointOfInterestDto()
          {
              Id=2,
              Name="bezaa tower",
              Description="fuck you man "
          },
      }          
                },
                      
            }
                ;

        }


    }




}
