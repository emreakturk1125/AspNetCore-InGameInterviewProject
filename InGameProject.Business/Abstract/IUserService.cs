using InGameProject.Core.Utilities.Results;
using InGameProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetUserListBL();
        IResult UserAddBL(User item);
        IDataResult<User> GetUserByIdBL(int userId);
        IDataResult<User> GetUserByEmailBL(string email);
        IResult UserDeleteBL(int userId);
        IResult UserUpdateBL(User item);
        IDataResult<User> GetUserByMail(string email);
    }
}
