using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Shop.Areas.Admin.Models

{
    public class LoginModel
    {
        [Required(ErrorMessage ="Mời nhập Tài khoản")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mời nhập Mật khẩu")]
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
    }
}