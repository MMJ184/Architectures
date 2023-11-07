using AutoMapper;
using InfinityCQRS.App.CommandResults.Users;
using InfinityCQRS.App.Commands.User;
using InfinityCQRS.Backend.Contracts;

namespace InfinityCQRS.App.Handlers
{
    public class DTOtoEntityMapper : Profile
    {
        public DTOtoEntityMapper()
        {
            CreateMap<ApplicationUser, AddUserResult>();
            CreateMap<ApplicationUser, AddUserCommand>().ReverseMap();

            CreateMap<ApplicationUser, UpdateUserResult>().ReverseMap();
            CreateMap<ApplicationUser, UpdateUserCommand>().ReverseMap();

            CreateMap<ApplicationUser, DeleteUserResult>().ReverseMap();
            CreateMap<ApplicationUser, DeleteUserCommand>().ReverseMap();

            CreateMap<ApplicationUser, GetUsersResult>().ReverseMap();
            CreateMap<ApplicationUser, GetUsersCommand>().ReverseMap();
        }
    }
}
