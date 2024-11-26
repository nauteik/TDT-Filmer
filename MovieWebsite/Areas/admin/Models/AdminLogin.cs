using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieWebsite.Areas.admin.Models
{
    public class AdminLogin
    {
        [Required(ErrorMessage = "Vui lòng nhập username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}