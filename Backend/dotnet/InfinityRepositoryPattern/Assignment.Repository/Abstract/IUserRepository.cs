using Assignment.Concrete.Abstract;
using Assignment.Database.Entities;

namespace Assignment.Repository.Abstract
{
    public interface IUserRepository : IRepositoryBase<Users>
    {
        Task<Users> GetUserByUserIdAsync(int userId, CancellationToken cancellationToken = default);

        Task<Users> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
