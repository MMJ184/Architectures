using Assignment.Database;
using Assignment.Database.Entities;
using Assignment.Repository.Abstract;
using Assignment.Repository.Concreate;

namespace Assignment.Repository.Concrete
{
    public class RolesRepository : RepositoryBase<Roles>, IRolesRepository
    {
        public RolesRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            
        }
    }
}
