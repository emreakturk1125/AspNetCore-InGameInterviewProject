using InGameProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.DataAccess.Abstract
{
    public interface IUserDal : IGenericDal<User>
    {
        User GetUserWithRoleByEmail(string email);
    }
}
