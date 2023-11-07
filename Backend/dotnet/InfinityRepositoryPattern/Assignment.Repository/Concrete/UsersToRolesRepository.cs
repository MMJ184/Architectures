using Assignment.Database;
using Assignment.Database.Entities;
using Assignment.Repository.Abstract;
using Assignment.Repository.Concreate;

namespace Assignment.Repository.Concrete
{
    public class UsersToRolesRepository : RepositoryBase<UserToRoles>, IUsersToRolesRepository
    {
        public UsersToRolesRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            
        }
    }
}
