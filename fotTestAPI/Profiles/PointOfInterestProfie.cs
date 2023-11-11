using AutoMapper;
using fotTestAPI.Entites;
using fotTestAPI.Model;

namespace fotTestAPI.Profiles
{
    public class PointOfInterestProfie:Profile
    {

        public PointOfInterestProfie() {

            CreateMap<Entites.PointOfInterests, Model.PointOfInterestDto>();
            CreateMap<Model.CreatePOintOfInterestDto, Entites.PointOfInterests>();
            CreateMap<Model.UpdatePointOfInterestDto, Entites.PointOfInterests>();

        }



    }
}
