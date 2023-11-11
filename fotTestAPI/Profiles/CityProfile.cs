
using AutoMapper;

namespace fotTestAPI.Profiles
{
    public class CityProfile: Profile
    {

        public CityProfile()

        {
            CreateMap<Entites.City, Model.CityDtoWithoutPointOfInterest>();
            CreateMap<Entites.City, Model.CitiesDto>();
            CreateMap<Model.UpdateAndCreateCityDto, Entites.City>();



        }



    }
}
