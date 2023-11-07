using Assignment.Database.Entities;
using Assignment.Repository.Abstract;
using Assignment.Service.Abstract;
using Assignment.Service.Common;
using Assignment.Service.DTO.User;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Assignment.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IUsersToRolesRepository _usersToRolesRepository;


        public UserService(IUserRepository userRepository, IMapper mapper, IRolesRepository rolesRepository,
            IUsersToRolesRepository usersToRolesRepository)
        {

            _userRepository = userRepository;
            _mapper = mapper;
            _rolesRepository = rolesRepository;
            _usersToRolesRepository = usersToRolesRepository;
        }

        public async Task<ResponseBase<UserResponse>> Add(AddUserRequest request, CancellationToken cancellationToken)
        {
            await _userRepository.BeginTransactionAsync();
            try
            {
                var user = _mapper.Map<Users>(request);
                user.PasswordHash = BC.HashPassword(request.Password);
                var userResult = await _userRepository.CreateAsync(user);

                var rolesData = await _rolesRepository.GetByCondition(x => x.Id == 8).Select(x => x.Id).ToListAsync();

                var roles = rolesData.Select(x => new UserToRoles()
                {
                    UsersId = userResult.Id,
                    RolesId = x,
                    CreatedBy = 1,
                    CreatedDate = DateTime.UtcNow,  
                }).ToList();

                await _usersToRolesRepository.CreateRangeAsync(roles);
                await _userRepository.CommitTransactionAsync();

                var result = _mapper.Map<UserResponse>(await _userRepository.GetUserByUserIdAsync(userResult.Id));
                return new ResponseBase<UserResponse>(result); ;
            }
            catch (Exception)
            {
                await _userRepository.RollbackTransactionAsync();
                return new ResponseBase<UserResponse>(null);
                throw;
            }
        }

        public async Task<ResponseBase<bool>> Delete(int id)
        {
            try
            {
                var user = await _userRepository.GetUserByUserIdAsync(id);

                await _userRepository.DeleteAsync(user);

                return new ResponseBase<bool>(true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseBase<List<UserResponse>>> GetAsync(GetUserRequest request)
        {
            var users = await _userRepository.GetAll()
                                             .Where(x => !x.IsDisabled)
                                             .ToListAsync();


            return new ResponseBase<List<UserResponse>>(_mapper.Map<List<UserResponse>>(users));
        }

        public async Task<ResponseBase<UserResponse>> GetById(int id)
        {
            var user = await _userRepository.GetUserByUserIdAsync(id);

            return new ResponseBase<UserResponse>(_mapper.Map<UserResponse>(user));
        }

        public async Task<ResponseBase<long>> GetCount()
        {
            var users = await _userRepository.GetAll().CountAsync();

            return new ResponseBase<long>(users);
        }

        public async Task<ResponseBase<UserResponse>> Update(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByUserIdAsync(request.Id, cancellationToken);

                if (user != null)
                {
                    user.Name = request.Name;
                    user.Email = request.Email;
                }

                await _userRepository.UpdateAsync(user);

                return new ResponseBase<UserResponse>(_mapper.Map<UserResponse>(user));

            }
            catch (Exception)
            {

                throw;
            }
        }

        


    }
}
