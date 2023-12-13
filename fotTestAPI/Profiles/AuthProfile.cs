using AutoMapper;
using fotTestAPI.Model.Authentication;

namespace fotTestAPI.Profiles
{
    public class AuthProfile :Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterModel, ApplicationUser>();


        }


    }
}
