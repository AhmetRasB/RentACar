using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericRepository;
using RentACarServer.Domain.Users;
using RentACarServer.Infrastructure.Context;

namespace RentACarServer.Infrastructure.Repositories
{
    internal class UserRepository : Repository<User,ApplicationDbContext>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
