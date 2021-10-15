using InGameProject.DataAccess.Abstract;
using InGameProject.DataAccess.Concrete;
using InGameProject.DataAccess.Concrete.Repositories;
using InGameProject.Entities.Concrete;
using InGameProject.Entities.Concrete.Dto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InGameProject.DataAccess.EntityFramework
{
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        public List<Product> GetListProductWithCategory()
        {
            using (var c = new Context())
            {
                return c.Products.Include(x => x.Category).ToList();
            }
        }

        public List<ProductCategoryDto> GetAllProductsBySp()
        {
            List<ProductCategoryDto> list = new List<ProductCategoryDto>();
            using (var cnn = new Context())
            {
                var con = cnn.Database.GetDbConnection();
                var cmm = con.CreateCommand();
                cmm.CommandType = System.Data.CommandType.StoredProcedure;
                cmm.CommandText = "[dbo].[SP_GetAllProducts]";
                cmm.Connection = con;
                con.Open();
                var reader = cmm.ExecuteReader();

                while (reader.Read())
                { 
                    list.Add(new ProductCategoryDto
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        ProductName = Convert.ToString(reader["ProductName"]),
                        ProductDescription = Convert.ToString(reader["ProductDescription"]),
                        ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                        ProductImageUrl = Convert.ToString(reader["ProductImageUrl"]),
                        IsActive = Convert.ToBoolean(reader["IsActive"]),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryName = Convert.ToString(reader["CategoryName"]),
                        CategoryDescription = Convert.ToString(reader["CategoryDescription"]),
                    });

                }
            }

            return list;

        }
        public Product GetProductWithCategoryById(int id)
        {
            using (var c = new Context())
            {
                return c.Products.Where(x => x.ProductId == id).Include(x => x.Category).FirstOrDefault();
            }

        }
    }
}
