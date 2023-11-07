using InfinityDapper.Database.Entities;
using InfinityDapper.DTO.Request.Authentication;
using InfinityDapper.DTO.Request.User;

namespace InfinityDapper.Repository.Abstract
{
    public interface IUserRepository
    {
        User Login(LoginRequest request);
        User GetUserMasterDetailsById(int UserId);
        User AddUpdate(AddUpdateUserRequest request);
        bool Delete(DeleteUserRequest request);
        User GetById(GetUserByIdRequest request);
        List<User> GetAsync(GetUsersRequest request);
        double GetCountAsync(GetUsersRequest request);
    }
}
