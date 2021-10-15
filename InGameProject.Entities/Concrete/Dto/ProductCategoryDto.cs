using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.Entities.Concrete.Dto
{
    public class ProductCategoryDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string ProductImageUrl { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
