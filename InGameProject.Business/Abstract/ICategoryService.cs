using InGameProject.Core.Utilities.Results;
using InGameProject.Entities.Concrete;
using InGameProject.Entities.Concrete.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<CategoryDto>> GetCategoryListByStoredProcedureBL();
        IDataResult<List<Category>> GetCategoryListBL();
        IResult CategoryAddBL(Category item);
        IDataResult<Category> GetCategoryByIdBL(int categoryId);
        IResult CategoryDeleteBL(int categoryId);
        IResult CategoryUpdateBL(Category item);
    }
}
