using InGameProject.Business.Abstract;
using InGameProject.Business.Constants;
using InGameProject.Core.Utilities.Results;
using InGameProject.DataAccess.Abstract;
using InGameProject.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace InGameProject.Business.Concrete
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserDal _userDal; 
        public AuthManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<string> Login(string userEmail, string password)
        {
            var result =_userDal.List(x => x.UserEmail == userEmail && x.UserPassword == password).Any();

            if (!result)
            {
                return new ErrorDataResult<string>(Messages.NotCreatedToken);
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("mysupersecretkeymysupersecretkey");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,userEmail)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = 
                new SigningCredentials (
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new SuccessDataResult<string>(tokenHandler.WriteToken(token), Messages.CreatedToken);
        }

        public IDataResult<string> NewPassword(string email)
        {
            var result = _userDal.Get(x => x.UserEmail == email);
            if (result == null)
            {
                return new ErrorDataResult<string>(Messages.UserNotFound);
            }
            var newPassword = Guid.NewGuid().ToString("d").Substring(1, 5);
            result.UserPassword = newPassword;
            _userDal.Update(result);

            return new SuccessDataResult<string>(newPassword, Messages.UserPasswordCreated);
        }

        public IDataResult<User> Register(User item)
        {
            if (_userDal.Get(x => x.UserEmail == item.UserEmail) != null)
            {
                return new ErrorDataResult<User>(Messages.UserAlreadyExists);
            }

            var user = new User
            {
                UserEmail = item.UserEmail,
                UserName = item.UserName,
                UserSurname = item.UserSurname,
                UserPassword = item.UserPassword, 
                UserRoleId = item.UserRoleId,
                IsActive = true
            };
            _userDal.Insert(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            if (_userDal.Get(x => x.UserEmail == email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
