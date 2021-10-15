using InGameProject.Core.Utilities.Results;
using InGameProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.Business.Abstract
{
    public interface IRoleService
    {
        IDataResult<List<Role>> GetRoleListBL();
        IResult RoleAddBL(Role item);
        IDataResult<Role> GetRoleByIdBL(int roleId);
        IResult RoleDeleteBL(int roleId);
        IResult RoleUpdateBL(Role item);
    }
}
