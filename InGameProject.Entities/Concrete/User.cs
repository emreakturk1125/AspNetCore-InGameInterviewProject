using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InGameProject.Entities.Concrete
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public int UserRoleId { get; set; }

        [ForeignKey("UserRoleId")]
        public virtual Role UserRole { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string UserSurname { get; set; }

        [StringLength(50)]
        public string UserEmail { get; set; } 

        [StringLength(50)]
        public string UserPassword { get; set; }
        public bool IsActive { get; set; }

    }
}
