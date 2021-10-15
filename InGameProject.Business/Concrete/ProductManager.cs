using InGameProject.Business.Abstract;
using InGameProject.Business.Constants;
using InGameProject.Core.Utilities.Business;
using InGameProject.Core.Utilities.Results;
using InGameProject.DataAccess.Abstract;
using InGameProject.Entities.Concrete;
using InGameProject.Entities.Concrete.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InGameProject.Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult ProductAddBL(Product item)
        {
            IResult result = BusınessRules.Run(CheckIfProductNameExists(item.ProductName),
               CheckIfCategoryNameLessThanThreeCharacters(item.ProductName));

            if (result != null)
            {
                return result;
            }

            _productDal.Insert(item);

            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult ProductDeleteBL(int productId)
        {
            Product result = _productDal.Get(p => p.ProductId == productId);
            if (result == null)
            {
                return new ErrorResult(Messages.ProductNotFound);
            }

            result.IsActive = false;
            _productDal.Update(result);
            return new SuccessResult(Messages.ProductDeleted);

        }

        public IResult ProductUpdateBL(Product item)
        {
              
            var result = _productDal.List(p => p.ProductId == item.ProductId).FirstOrDefault();

            if (result == null)
            {
                return new ErrorResult(Messages.ProductNotFound);
            }

            _productDal.Update(item);
            return new SuccessResult(Messages.ProductUpdated);

        }

        public IDataResult<Product> GetProductByIdBL(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));

        }

        public IDataResult<List<Product>> GetProductListBL()
        {
            return new SuccessDataResult<List<Product>>(_productDal.List(), Messages.ProductsListed);

        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.Get(p => p.ProductName == productName);
            if (result != null)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }


        private IResult CheckIfCategoryNameLessThanThreeCharacters(string productName)
        {
            if (productName.Length < 3)
            {
                return new ErrorResult(Messages.ProductNameCharacterLength);
            }
            return new SuccessResult();
        }

        public IDataResult<List<Product>> GetProductListWithCategoryBL()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetListProductWithCategory(), Messages.ProductsListed);

        }

        public IDataResult<Product> GetProductWithCategoryByIdBL(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.GetProductWithCategoryById(productId));


        }

        public IDataResult<List<ProductCategoryDto>> GetProductListByStoredProcedureBL()
        {
            return new SuccessDataResult<List<ProductCategoryDto>>(_productDal.GetAllProductsBySp(), Messages.ProductsListed);

        }

        public Product GetProductForUnitTestByIdBL(int productId)
        {
            return _productDal.Get(x => x.ProductId == productId);
        }

        public List<Product> GetProductListForUnitTestBL()
        {
            return _productDal.List();
        }
    }
}
