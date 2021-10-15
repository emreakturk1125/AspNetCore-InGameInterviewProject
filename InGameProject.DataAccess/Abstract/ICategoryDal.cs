using InGameProject.Entities.Concrete;
using InGameProject.Entities.Concrete.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.DataAccess.Abstract
{
    public interface ICategoryDal : IGenericDal<Category>
    {
        List<CategoryDto> GetAllCategoriesBySp();

    }
}
