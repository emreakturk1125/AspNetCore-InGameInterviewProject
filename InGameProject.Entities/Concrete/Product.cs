using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InGameProject.Entities.Concrete
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [StringLength(250)]
        public string ProductImageUrl { get; set; }
        public decimal ProductPrice { get; set; }

        [StringLength(500)]
        public string ProductDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
