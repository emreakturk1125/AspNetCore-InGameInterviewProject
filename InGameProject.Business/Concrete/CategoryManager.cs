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
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IResult CategoryAddBL(Category item)
        {

            IResult result = BusınessRules.Run(CheckIfCategoryNameExists(item.CategoryName),
             CheckIfCategoryNameLessThanThreeCharacters(item.CategoryName));

            if (result != null)
            {
                return result;
            }

            _categoryDal.Insert(item);

            return new SuccessResult(Messages.ProductAdded);
             
        }

        public IResult CategoryDeleteBL(int categoryId)
        {
            Category result = _categoryDal.Get(p => p.CategoryId == categoryId);
            if (result == null)
            {
                return new ErrorResult(Messages.CategoryNotFound);
            }
            result.IsActive = false;
            _categoryDal.Update(result);

            return new SuccessResult(Messages.CategoryDeleted);
        }

        public IResult CategoryUpdateBL(Category item)
        {

            var result = _categoryDal.List(p => p.CategoryId == item.CategoryId).FirstOrDefault();

            if (result != null)
            {
                _categoryDal.Update(item);
            }

            return new SuccessResult(Messages.CategoryUpdated);

        }

        public IDataResult<Category> GetCategoryByIdBL(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(x => x.CategoryId == categoryId));
        }
  
        public IDataResult<List<Category>> GetCategoryListBL()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.List(), Messages.CategoriesListed);
        }

        public IDataResult<List<CategoryDto>> GetCategoryListByStoredProcedureBL()
        {
            return new SuccessDataResult<List<CategoryDto>>(_categoryDal.GetAllCategoriesBySp(), Messages.CategoriesListed);
        }

        private IResult CheckIfCategoryNameExists(string categoryName)
        {
            var result = _categoryDal.Get(p => p.CategoryName == categoryName);
            if (result != null)
            {
                return new ErrorResult(Messages.CategoryNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryNameLessThanThreeCharacters(string categoryName)
        {
            if (categoryName.Length < 3)
            {
                return new ErrorResult(Messages.CategoryNameCharacterLength);
            }
            return new SuccessResult();
        }
    }
}
