using fotTestAPI.Model;
using AutoMapper;

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
