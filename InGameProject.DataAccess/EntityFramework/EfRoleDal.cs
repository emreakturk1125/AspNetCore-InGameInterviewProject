using InGameProject.DataAccess.Abstract;
using InGameProject.DataAccess.Concrete.Repositories;
using InGameProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.DataAccess.EntityFramework
{
    public class EfRoleDal : GenericRepository<Role>, IRoleDal
    {
    }
}
