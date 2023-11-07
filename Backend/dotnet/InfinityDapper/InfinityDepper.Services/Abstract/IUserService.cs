using InfinityDapper.DTO.Request.User;
using InfinityDapper.DTO.Response.Users;

namespace InfinityDapper.Services.Abstract
{
    public interface IUserService
    {
        UserResponse AddUpdate(AddUpdateUserRequest request);
        bool Delete(DeleteUserRequest request);
        UserResponse GetById(GetUserByIdRequest request);
        List<UserResponse> GetAsync(GetUsersRequest request);
        double GetCountAsync(GetUsersRequest request);
    }
}
