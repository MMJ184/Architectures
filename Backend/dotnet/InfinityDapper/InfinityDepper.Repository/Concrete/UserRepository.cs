using Dapper;
using InfinityDapper.Dapper;
using InfinityDapper.Database.Entities;
using InfinityDapper.DTO.Request.Authentication;
using InfinityDapper.DTO.Request.User;
using InfinityDapper.DTO.Response.Users;
using InfinityDapper.Repository.Abstract;
using System.Data;

namespace InfinityDapper.Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapperContext _context;

        public UserRepository(IDapperContext context)
        {
            _context = context;
        }

        public User Login(LoginRequest request)
        {
            var sp_params = new DynamicParameters();
            sp_params.Add("@Email", request.Email, DbType.String);
            sp_params.Add("@PasswordHash", request.Password, DbType.String);

            return _context.Get<User>("UserAuthentication", sp_params);
        }

        public User GetUserMasterDetailsById(int UserId)
        {
            User model = new User();
            try
            {
                var sp_params = new DynamicParameters();
                sp_params.Add("@UserId", UserId, DbType.Int32);
                model = _context.Get<User>("Usp_GetUserMasterDetailsById", sp_params);
                model = model == null ? new User() : model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model;
        }

        public User AddUpdate(AddUpdateUserRequest request)
        {
            var xmlModel = XmlUtility.XmlSerializeToString(request);
            
            var sp_params = new DynamicParameters();
            sp_params.Add("@XML", xmlModel, DbType.Xml);
            sp_params.Add("@Id", request.Id, DbType.Int32);

            var newUser = _context.Insert<User>("AddUpdateUser", sp_params);

            return newUser;
        }

        public bool Delete(DeleteUserRequest request)
        {
            var sp_params = new DynamicParameters();
            sp_params.Add("@Id", request.Id, DbType.Int32);

            var newUser = _context.Update<User>("DeleteUser", sp_params);

            return newUser.IsDisabled;
        }

        public User GetById(GetUserByIdRequest request)
        {
            var sp_params = new DynamicParameters();
            sp_params.Add("@Id", request.Id, DbType.Int32);

            var response = _context.Get<User>("GetUserById", sp_params);

            return response;
        }

        public List<User> GetAsync(GetUsersRequest request)
        {
            var sp_params = new DynamicParameters();

            var response = _context.GetAll<User>("GetUsers", sp_params);

            return response;
        }
        public double GetCountAsync(GetUsersRequest request)
        {
            var sp_params = new DynamicParameters();

            var response = _context.GetAll<User>("GetUsers", sp_params);

            return response.Count;
        }
    }
}
