using InGameProject.Core.Utilities.Results;
using InGameProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.Business.Abstract
{
    public interface IAuthManager
    {
        IDataResult<string> Login(string userEmail, string password);
        IResult UserExists(string email);
        IDataResult<User> Register(User user);
        IDataResult<string> NewPassword(string user);

    }
}
