using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InGameProject.Entities.Concrete
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public int? MainCategoryId { get; set; }

        [ForeignKey("MainCategoryId")]
        public virtual Category MainCategory { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        [StringLength(200)]
        public string CategoryDescription { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
