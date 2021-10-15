using InGameProject.Business.Abstract;
using InGameProject.Business.Constants;
using InGameProject.Core.Utilities.Business;
using InGameProject.Core.Utilities.Results;
using InGameProject.DataAccess.Abstract;
using InGameProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InGameProject.Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult UserAddBL(User item)
        {
            IResult result = BusınessRules.Run(CheckIfUserEmailExists(item.UserEmail),
               CheckIfUserEmailLessThanThreeCharacters(item.UserEmail));

            if (result != null)
            {
                return result;
            }

            _userDal.Insert(item);

            return new SuccessResult(Messages.UserAdded);
        }

        public IResult UserDeleteBL(int UserId)
        {
            User result = _userDal.Get(p => p.UserId == UserId);
            if (result == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            result.IsActive = false;
            _userDal.Update(result);
            return new SuccessResult(Messages.UserDeleted);

        }

        public IResult UserUpdateBL(User item)
        {

            var result = _userDal.List(p => p.UserId == item.UserId).FirstOrDefault();

            if (result == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            _userDal.Update(item);
            return new SuccessResult(Messages.UserUpdated);

        }

        public IDataResult<User> GetUserByIdBL(int UserId)
        {
            return new SuccessDataResult<User>(_userDal.Get(p => p.UserId == UserId));

        }

        public IDataResult<List<User>> GetUserListBL()
        {
            return new SuccessDataResult<List<User>>(_userDal.List(), Messages.UsersListed);

        }
        public IDataResult<User> GetUserByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.GetUserWithRoleByEmail(email));

        }

        private IResult CheckIfUserEmailExists(string userEmail)
        {
            var result = _userDal.Get(p => p.UserEmail == userEmail);
            if (result != null)
            {
                return new ErrorResult(Messages.UserNameAlreadyExists);
            }
            return new SuccessResult();
        }


        private IResult CheckIfUserEmailLessThanThreeCharacters(string userEmail)
        {
            if (userEmail.Length < 13)
            {
                return new ErrorResult(Messages.UserNameCharacterLength);
            }
            return new SuccessResult();
        }

        public IDataResult<User> GetUserByEmailBL(string email)
        {
            return new SuccessDataResult<User>(_userDal.GetUserWithRoleByEmail(email));

        }
 
    }
}
