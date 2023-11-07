using Assignment.Service.Common;
using Assignment.Service.DTO.User;

namespace Assignment.Service.Abstract
{
    public interface IUserService
    {
        Task<ResponseBase<UserResponse>> Add(AddUserRequest request, CancellationToken cancellationToken);

        Task<ResponseBase<UserResponse>> Update(UpdateUserRequest request, CancellationToken cancellationToken);

        Task<ResponseBase<bool>> Delete(int id);

        Task<ResponseBase<UserResponse>> GetById(int id);

        Task<ResponseBase<long>> GetCount();

        Task<ResponseBase<List<UserResponse>>> GetAsync(GetUserRequest request);
    }
}
