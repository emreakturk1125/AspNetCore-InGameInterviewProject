using InGameProject.DataAccess.Abstract;
using InGameProject.DataAccess.Concrete;
using InGameProject.DataAccess.Concrete.Repositories;
using InGameProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InGameProject.DataAccess.EntityFramework
{
    public class EfUserDal : GenericRepository<User>, IUserDal
    {
        public User GetUserWithRoleByEmail(string email)
        {
            using (var c = new Context())
            {
                return c.Users.Include(x => x.UserRole).Where(x => x.UserEmail == email).FirstOrDefault();
            }

        }
    }
}
