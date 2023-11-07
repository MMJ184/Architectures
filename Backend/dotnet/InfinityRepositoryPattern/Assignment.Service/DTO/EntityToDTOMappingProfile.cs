using Assignment.Database.Entities;
using Assignment.Service.DTO.Account;
using Assignment.Service.DTO.User;
using AutoMapper;

namespace Assignment.Service.DTO
{
    public class EntityToDTOMappingProfile : Profile
    { 
        public override string ProfileName
        {
            get
            {
                return "EntityToDTOMappingProfile";
            }
        }

        public EntityToDTOMappingProfile()
        {
            ConfigureMappings();
        }

        private void ConfigureMappings()
        {

            #region Account
            CreateMap<Users, AuthenticationRequest>().ReverseMap();
            CreateMap<Users, AuthenticationResponse>().ReverseMap();
            #endregion

            #region User

            CreateMap<Users, AddUserRequest>().ReverseMap();
            CreateMap<Users, UpdateUserRequest>().ReverseMap();
            CreateMap<Users, UserResponse>().ReverseMap();

            #endregion
        }
    }
}
