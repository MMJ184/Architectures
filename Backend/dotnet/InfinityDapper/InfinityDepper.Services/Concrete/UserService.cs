using InfinityDapper.DTO.Request.User;
using InfinityDapper.DTO.Response.Users;
using InfinityDapper.Repository.Abstract;
using InfinityDapper.Services.Abstract;
using Microsoft.Extensions.Configuration;

namespace InfinityDapper.Services.Concrete
{
    public class UserService : IUserService
    {
        #region Variable Declaration & Constructor
        private readonly IUserRepository _repository;
        private readonly IConfiguration _config;

        public UserService(IUserRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }
        #endregion

        public UserResponse AddUpdate(AddUpdateUserRequest request)
        {
            try
            {
                var user = _repository.AddUpdate(request);

                if (user != null)
                {
                    var result = new UserResponse()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        MobileNumber = user.MobileNumber,
                        PasswordHash = user.PasswordHash,
                        IsDisabled = user.IsDisabled,
                        CreatedBy = user.CreatedBy,
                        CreatedDate = user.CreatedDate,
                        ModifiedBy = user.ModifiedBy,
                        ModifiedDate = user.ModifiedDate
                    };

                    return result;
                }
                else
                {
                    throw new Exception("Error");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Your process couldn't be complete at this time. Please try after some time.");
            }
        }

        public bool Delete(DeleteUserRequest request)
        {
            try
            {
                var user = _repository.Delete(request);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Your process couldn't be complete at this time. Please try after some time.");
            }
        }

        public UserResponse GetById(GetUserByIdRequest request)
        {
            try
            {
                var user = _repository.GetById(request);

                if (user != null)
                {
                    var result = new UserResponse()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        MobileNumber = user.MobileNumber,
                        PasswordHash = user.PasswordHash,
                        IsDisabled = user.IsDisabled,
                        CreatedBy = user.CreatedBy,
                        CreatedDate = user.CreatedDate,
                        ModifiedBy = user.ModifiedBy,
                        ModifiedDate = user.ModifiedDate
                    };

                    return result;
                }
                else
                {
                    throw new Exception("user not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Your process couldn't be complete at this time. Please try after some time.");
            }
        }

        public List<UserResponse> GetAsync(GetUsersRequest request)
        {
            try
            {
                var users = _repository.GetAsync(request);

                if (users != null)
                {
                    var response = new List<UserResponse>();
                    foreach (var user in users)
                    {
                        var result = new UserResponse()
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            MobileNumber = user.MobileNumber,
                            PasswordHash = user.PasswordHash,
                            IsDisabled = user.IsDisabled,
                            CreatedBy = user.CreatedBy,
                            CreatedDate = user.CreatedDate,
                            ModifiedBy = user.ModifiedBy,
                            ModifiedDate = user.ModifiedDate
                        };
                        response.Add(result);
                    }
                    return response;
                }
                else
                {
                    throw new Exception("users not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Your process couldn't be complete at this time. Please try after some time.");
            }
        }

        public double GetCountAsync(GetUsersRequest request)
        {
            try
            {
                return _repository.GetCountAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception("null count");
            }
        }
    }
}
