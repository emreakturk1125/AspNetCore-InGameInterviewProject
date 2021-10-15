using InGameProject.Core.Utilities.Results;
using InGameProject.Entities.Concrete;
using InGameProject.Entities.Concrete.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<ProductCategoryDto>> GetProductListByStoredProcedureBL();
        IDataResult<List<Product>> GetProductListBL();
        List<Product> GetProductListForUnitTestBL();
        IDataResult<List<Product>> GetProductListWithCategoryBL();
        IResult ProductAddBL(Product item);
        IDataResult<Product> GetProductByIdBL(int productId);
        Product GetProductForUnitTestByIdBL(int productId);
        IDataResult<Product> GetProductWithCategoryByIdBL(int productId);
        IResult ProductDeleteBL(int productId);
        IResult ProductUpdateBL(Product item);
    }
}
