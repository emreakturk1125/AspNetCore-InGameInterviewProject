using InGameProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGameProject.WebUI.Models
{
    public class UserForLogin
    {
        public User User { get; set; }
        public bool IsRemember { get; set; }
    }

}
