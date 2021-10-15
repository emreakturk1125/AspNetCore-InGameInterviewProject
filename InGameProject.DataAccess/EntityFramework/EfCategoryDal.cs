using InGameProject.DataAccess.Abstract;
using InGameProject.DataAccess.Concrete;
using InGameProject.DataAccess.Concrete.Repositories;
using InGameProject.Entities.Concrete;
using InGameProject.Entities.Concrete.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.DataAccess.EntityFramework
{
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        public List<CategoryDto> GetAllCategoriesBySp()
        {
            List<CategoryDto> list = new List<CategoryDto>();
            using (var cnn = new Context())
            {
                var con = cnn.Database.GetDbConnection();
                var cmm = con.CreateCommand();
                cmm.CommandType = System.Data.CommandType.StoredProcedure;
                cmm.CommandText = "[dbo].[SP_GetAllCategories]";
                cmm.Connection = con;
                con.Open();
                var reader = cmm.ExecuteReader();


                int? categoryId = null, subCategoryId = null;
                string categoryName = null, categoryDescription = null, subCategoryName = null, subCategoryDescription = null;
                bool isActive = false, subCategoryIsActive = false;

                while (reader.Read())
                {

                    if (!reader.IsDBNull(reader.GetOrdinal("CategoryId")) )
                        categoryId = Convert.ToInt32(reader["CategoryId"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("CategoryName")))
                        categoryName = Convert.ToString(reader["CategoryName"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("CategoryDescription")))
                        categoryDescription = Convert.ToString(reader["CategoryDescription"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("IsActive")))
                        isActive = Convert.ToBoolean(reader["IsActive"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("SubCategoryId")))
                        subCategoryId = Convert.ToInt32(reader["SubCategoryId"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("SubCategoryName")))
                        subCategoryName = Convert.ToString(reader["SubCategoryName"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("SubCategoryDescription")))
                        subCategoryDescription = Convert.ToString(reader["SubCategoryDescription"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("SubCategoryIsActive")))
                        subCategoryIsActive = Convert.ToBoolean(reader["SubCategoryIsActive"]);


                    list.Add(new CategoryDto
                    {
                         CategoryId = categoryId,
                         CategoryName = categoryName,
                         CategoryDescription = categoryDescription,
                         IsActive = isActive,
                         SubCategoryId = subCategoryId,
                         SubCategoryName = subCategoryName,
                         SubCategoryDescription = subCategoryDescription,
                         SubCategoryIsActive = subCategoryIsActive
                    });

                }
            }

            return list;

        }
         
    }
}
