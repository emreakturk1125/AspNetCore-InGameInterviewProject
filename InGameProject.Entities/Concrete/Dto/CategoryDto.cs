using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.Entities.Concrete.Dto
{
    public class CategoryDto
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; } 
        public string CategoryDescription { get; set; }
        public bool IsActive { get; set; }
        public int? SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryDescription { get; set; }
        public bool SubCategoryIsActive { get; set; }

    }
}
