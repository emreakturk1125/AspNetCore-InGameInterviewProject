using InGameProject.Core.Utilities.Results;
using InGameProject.Entities.Concrete;
using InGameProject.Entities.Concrete.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.DataAccess.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        List<Product> GetListProductWithCategory();
        List<ProductCategoryDto> GetAllProductsBySp();
        Product GetProductWithCategoryById(int id);
    }
}
