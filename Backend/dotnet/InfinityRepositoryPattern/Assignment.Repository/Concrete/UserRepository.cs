using Assignment.Database;
using Assignment.Database.Entities;
using Assignment.Repository.Abstract;
using Assignment.Repository.Concreate;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repository.Concrete
{
    public class UserRepository : RepositoryBase<Users>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        public async Task<Users> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await GetByCondition(x => x.Email == email
                                          && x.IsDisabled == false)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Users> GetUserByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await GetByCondition(x => x.Id == userId
                                         && x.IsDisabled == false)
                        .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
