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
    public class RoleManager : IRoleService
    {
        IRoleDal _roleDal;

        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }

        public IResult RoleAddBL(Role item)
        {
            IResult result = BusınessRules.Run(CheckIfRoleNameExists(item.RoleName),
               CheckIfCategoryNameLessThanThreeCharacters(item.RoleName));

            if (result != null)
            {
                return result;
            }

            _roleDal.Insert(item);

            return new SuccessResult(Messages.RoleAdded);
        }

        public IResult RoleDeleteBL(int roleId)
        {
            Role result = _roleDal.Get(p => p.RoleId == roleId);
            if (result == null)
            {
                return new ErrorResult(Messages.RoleNotFound);
            }

            result.IsActive = false;
            _roleDal.Update(result);
            return new SuccessResult(Messages.RoleDeleted);

        }

        public IResult RoleUpdateBL(Role item)
        {

            var result = _roleDal.List(p => p.RoleId == item.RoleId).FirstOrDefault();

            if (result == null)
            {
                return new ErrorResult(Messages.RoleNotFound);
            }

            _roleDal.Update(item);
            return new SuccessResult(Messages.RoleUpdated);

        }

        public IDataResult<Role> GetRoleByIdBL(int roleId)
        {
            return new SuccessDataResult<Role>(_roleDal.Get(p => p.RoleId == roleId));

        }

        public IDataResult<List<Role>> GetRoleListBL()
        {
            return new SuccessDataResult<List<Role>>(_roleDal.List(), Messages.RolesListed);

        }

        private IResult CheckIfRoleNameExists(string roleName)
        {
            var result = _roleDal.Get(p => p.RoleName == roleName);
            if (result != null)
            {
                return new ErrorResult(Messages.RoleNameAlreadyExists);
            }
            return new SuccessResult();
        }


        private IResult CheckIfCategoryNameLessThanThreeCharacters(string RoleName)
        {
            if (RoleName.Length < 3)
            {
                return new ErrorResult(Messages.RoleNameCharacterLength);
            }
            return new SuccessResult();
        }
    }
}
