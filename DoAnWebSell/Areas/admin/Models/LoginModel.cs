using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnWebSell.Areas.admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Mời bạn nhập username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mời bạn nhập password")]
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
        public bool Power { get; set; }
    }
}